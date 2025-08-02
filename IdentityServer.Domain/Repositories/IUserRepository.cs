using IdentityServer.Domain.Entities;

namespace IdentityServer.Domain.Repositories;

public interface IUserRepository
{
    Task<ApplicationUser?> GetByIdAsync(string id);
    Task<ApplicationUser?> GetByEmailAsync(string email);
    Task<ApplicationUser?> CreateAsync(ApplicationUser user, string password);
    Task<bool> UpdateAsync(ApplicationUser user);
    Task<bool> DeleteAsync(string id);
    Task<bool> ValidateCredentialsAsync(string email, string password);
}