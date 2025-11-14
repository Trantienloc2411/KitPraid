using IdentityServer.Application.Dtos;
using IdentityServer.Domain;
using IdentityServer.Domain.Entities;
using IdentityServer.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Application.Services;

public class UserService(IUserRepository userRepository) : IUserService
{

    public async Task<IdentityServer.Domain.OperationResult<ApplicationUser>> GetUserAsync(string userId)
    {
        return await userRepository.GetByIdAsync(userId);
    }


    public async Task<IdentityServer.Domain.OperationResult<ApplicationUser>> GetUserByEmailAsync(string email)
    {
        return await userRepository.GetByEmailAsync(email.ToLower());
    }


    public async Task<IdentityServer.Domain.OperationResult<ApplicationUser>> CreateUserAsync(CreateUserRequest userRequest)
    {
        var user = new ApplicationUser
        {
            FirstName = userRequest.FirstName,
            LastName = userRequest.LastName,
            CreatedAt = DateTime.Now,
            AccessFailedCount = 0,
            Email = userRequest.Email.ToLower(),
            EmailConfirmed = true,
            Id = Guid.NewGuid().ToString(),
            IsActive = true,
            PhoneNumber = userRequest.PhoneNumber,
            UserName = userRequest.UserName,
        };

        var result = await userRepository.CreateAsync(user, userRequest.Password, userRequest.Role);
        return result;
    }

    public async Task<IdentityServer.Domain.OperationResult<bool>> ValidateCredentialsAsync(string username, string password)
    {
        return await userRepository.ValidateCredentialsAsync(username.ToLower(), password);
    }

    public async Task<IdentityServer.Domain.OperationResult<ApplicationUser>> UpdateUserAsync(UpdateUserRequest userRequest, string userId)
    {
        var getOp = await userRepository.GetByIdAsync(userId);
        if (!getOp.Success || getOp.Data is null)
            return Domain.OperationResult<ApplicationUser>.Fail("User not found.");

        var user = getOp.Data;
        user.FirstName = userRequest.FirstName;
        user.LastName = userRequest.LastName;
        user.PhoneNumber = userRequest.PhoneNumber;
        user.Email = userRequest.Email;
        user.UserName = userRequest.UserName;

        return await userRepository.UpdateAsync(user);
    }

    public async Task<IdentityServer.Domain.OperationResult<bool>> DeactivateUserAsync(string userId)
    {
        var getOp = await userRepository.GetByIdAsync(userId);
        if (!getOp.Success || getOp.Data is null)
            return Domain.OperationResult<bool>.Fail("User not found.");

        var user = getOp.Data;
        user.IsActive = false;
        var updateOp = await userRepository.UpdateAsync(user);
        return updateOp.Success
            ? Domain.OperationResult<bool>.Ok(true)
            : Domain.OperationResult<bool>.Fail(updateOp.Error ?? "Failed to deactivate user.");
    }

    public async Task<OperationResult<bool>> ChangePassword(string userId, string oldPassword, string newPassword)
    {
        var userFind = await userRepository.GetByIdAsync(userId);
        if (!userFind.Success || userFind.Data is null)
        {
            return OperationResult<bool>.Fail("User not found");
        }
        else
        {
            return await userRepository.ChangePassword(userId, oldPassword, newPassword);
        }
    }
}