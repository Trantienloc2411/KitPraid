using IdentityServer.Domain;
using IdentityServer.Domain.Entities;

namespace IdentityServer.Application.Services;

public interface IAccountService
{
    Task<OperationResult<bool>> SignInAsync(string email, string password, bool isPersistent, bool lockoutOnFailure);
    Task SignOutAsync();
    Task<OperationResult<ApplicationUser>> GetUserByEmailAsync(string email);
    Task<OperationResult<ApplicationUser>> GetCurrentUserAsync(System.Security.Claims.ClaimsPrincipal principal);
}

