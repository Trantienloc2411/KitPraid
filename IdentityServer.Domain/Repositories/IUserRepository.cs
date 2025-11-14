using IdentityServer.Domain.Entities;

namespace IdentityServer.Domain.Repositories;

public interface IUserRepository
{
    Task<IdentityServer.Domain.OperationResult<ApplicationUser>> GetByIdAsync(string id);
    Task<IdentityServer.Domain.OperationResult<ApplicationUser>> GetByEmailAsync(string email);
    Task<IdentityServer.Domain.OperationResult<ApplicationUser>> CreateAsync(ApplicationUser user, string password, string role);
    Task<IdentityServer.Domain.OperationResult<ApplicationUser>> UpdateAsync(ApplicationUser user);
    Task<IdentityServer.Domain.OperationResult<bool>> DeleteAsync(string id);
    Task<IdentityServer.Domain.OperationResult<bool>> ValidateCredentialsAsync(string email, string password);

    Task<OperationResult<bool>> ChangePassword(string userId, string currentPassword, string newPassword);
}