using IdentityServer.Application.Dtos;
using IdentityServer.Domain.Entities;

namespace IdentityServer.Application.Services;

public class UserService : IUserService
{
    public async Task<ApplicationUser> GetUserAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<ApplicationUser> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<ApplicationUser> CreateUserAsync(CreateUserRequest userRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ValidateCredentialsAsync(string username, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateUserAsync(UpdateUserRequest userRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeactivateUserAsync(string userId)
    {
        throw new NotImplementedException();
    }
}