using System.Data;
using Azure.Core;
using IdentityServer.Application.Dtos;
using IdentityServer.Domain;
using IdentityServer.Domain.Entities;
using IdentityServer.Domain.Repositories;
using IdentityServer.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser?> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly DbContext _dbContext;

    public UserRepository(UserManager<ApplicationUser?> userManager, RoleManager<IdentityRole> roleManager, DbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _roleManager = roleManager;
    }
    public async Task<Domain.OperationResult<ApplicationUser>> GetByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        return user is null
            ? Domain.OperationResult<ApplicationUser>.Fail($"User with ID {id} not found.")
            : Domain.OperationResult<ApplicationUser>.Ok(user);
    }

    public async Task<Domain.OperationResult<ApplicationUser>> GetByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user is null
            ? Domain.OperationResult<ApplicationUser>.Fail($"User with email {email} not found.")
            : Domain.OperationResult<ApplicationUser>.Ok(user);
    }

    public async Task<Domain.OperationResult<ApplicationUser>> CreateAsync(ApplicationUser userParams, string password, string role)
    {
        var userFind = _dbContext.Users.FirstOrDefault(c => c.PhoneNumber == userParams.PhoneNumber);
        if (userFind is not null)
            return Domain.OperationResult<ApplicationUser>.Fail($"This phone number {userParams.PhoneNumber} already exists.");

        var result = await _userManager.CreateAsync(userParams, password);
        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            return Domain.OperationResult<ApplicationUser>.Fail($"There was an issue saving the user: {errors}");
        }

        if (!await _roleManager.RoleExistsAsync(role))
            await _roleManager.CreateAsync(new IdentityRole(role));
        await _userManager.AddToRoleAsync(userParams, role);
        var createdOp = await GetByIdAsync(userParams.Id);
        if (!createdOp.Success || createdOp.Data is null)
            return Domain.OperationResult<ApplicationUser>.Fail("User created but could not be retrieved afterwards.");

        return Domain.OperationResult<ApplicationUser>.Ok(createdOp.Data);
    }

    public async Task<Domain.OperationResult<ApplicationUser>> UpdateAsync(ApplicationUser user)
    {
        var result = await _userManager.UpdateAsync(user);


        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            return Domain.OperationResult<ApplicationUser>.Fail($"Failed to update user: {errors}");
        }

        var updated = await _userManager.FindByIdAsync(user.Id);
        return updated is null
            ? Domain.OperationResult<ApplicationUser>.Fail("User updated but could not be retrieved afterwards.")
            : Domain.OperationResult<ApplicationUser>.Ok(updated);
    }

    public async Task<Domain.OperationResult<bool>> DeleteAsync(string id)
    {
        var getOp = await GetByIdAsync(id);
        if (!getOp.Success || getOp.Data is null)
            return Domain.OperationResult<bool>.Fail($"User with ID {id} not found.");

        var result = await _userManager.DeleteAsync(getOp.Data);
        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            return Domain.OperationResult<bool>.Fail($"Failed to delete user: {errors}");
        }

        return Domain.OperationResult<bool>.Ok(true);
    }

    public async Task<Domain.OperationResult<bool>> ValidateCredentialsAsync(string email, string password)
    {
        var getOp = await GetByEmailAsync(email);
        if (!getOp.Success || getOp.Data is null) return Domain.OperationResult<bool>.Fail("Invalid credentials.");

        var user = getOp.Data;
        if (!user.IsActive) return Domain.OperationResult<bool>.Fail("User is not active.");

        var result = await _userManager.CheckPasswordAsync(user, password);
        return result
            ? Domain.OperationResult<bool>.Ok(true)
            : Domain.OperationResult<bool>.Fail("Invalid credentials.");
    }

    public Task<Domain.OperationResult<ApplicationUser>> CreateAsync(ApplicationUser user, string password)
    {
        // Convenience overload - supply empty role if caller doesn't provide one
        return CreateAsync(user, password, string.Empty);
    }

    public async Task<OperationResult<bool>> ChangePassword(string userId, string currentPassword, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        if (!result.Succeeded || result is null)
        {
            return OperationResult<bool>.Fail("Failed when changing your password!");
        }
        else
        {
            return OperationResult<bool>.Ok(true);
        }

    }
}