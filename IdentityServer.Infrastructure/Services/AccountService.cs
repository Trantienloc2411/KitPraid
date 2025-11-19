using System.Security.Claims;
using IdentityServer.Application.Services;
using IdentityServer.Domain;
using IdentityServer.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountService(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<OperationResult<bool>> SignInAsync(
        string email, 
        string password, 
        bool isPersistent, 
        bool lockoutOnFailure)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return OperationResult<bool>.Fail("Invalid login attempt.");
        }

        if (!user.IsActive)
        {
            return OperationResult<bool>.Fail("Account is deactivated.");
        }

        var result = await _signInManager.PasswordSignInAsync(
            user.UserName ?? user.Email ?? string.Empty, 
            password, 
            isPersistent, 
            lockoutOnFailure);

        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
            {
                return OperationResult<bool>.Fail("Account is locked out.");
            }
            if (result.IsNotAllowed)
            {
                return OperationResult<bool>.Fail("Account is not allowed to sign in.");
            }
            return OperationResult<bool>.Fail("Invalid login attempt.");
        }

        return OperationResult<bool>.Ok(true);
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<OperationResult<ApplicationUser>> GetUserByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return OperationResult<ApplicationUser>.Fail("User not found.");
        }

        return OperationResult<ApplicationUser>.Ok(user);
    }

    public async Task<OperationResult<ApplicationUser>> GetCurrentUserAsync(ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal);
        if (user == null)
        {
            return OperationResult<ApplicationUser>.Fail("User not found.");
        }

        return OperationResult<ApplicationUser>.Ok(user);
    }
}

