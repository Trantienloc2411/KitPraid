using IdentityServer.Application.Dtos;
using IdentityServer.Domain.Entities;

namespace IdentityServer.Application.Services;

public interface IUserService
{
    Task<ApplicationUser> GetUserAsync(string userId);
    Task<ApplicationUser> GetUserByEmailAsync(string email);
    Task<ApplicationUser> CreateUserAsync(CreateUserRequest userRequest);
    Task<bool> ValidateCredentialsAsync(string username, string password);
    Task<bool> UpdateUserAsync(UpdateUserRequest userRequest);
    Task<bool> DeactivateUserAsync(string userId);
}