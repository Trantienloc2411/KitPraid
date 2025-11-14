using IdentityServer.Application.Dtos;
using IdentityServer.Domain;
using IdentityServer.Domain.Entities;

namespace IdentityServer.Application.Services;

public interface IUserService
{
    Task<IdentityServer.Domain.OperationResult<ApplicationUser>> GetUserAsync(string userId);
    Task<IdentityServer.Domain.OperationResult<ApplicationUser>> GetUserByEmailAsync(string email);
    Task<IdentityServer.Domain.OperationResult<ApplicationUser>> CreateUserAsync(CreateUserRequest userRequest);
    Task<IdentityServer.Domain.OperationResult<bool>> ValidateCredentialsAsync(string username, string password);
    Task<IdentityServer.Domain.OperationResult<ApplicationUser>> UpdateUserAsync(UpdateUserRequest userRequest, string userId);
    Task<IdentityServer.Domain.OperationResult<bool>> DeactivateUserAsync(string userId);
    Task<OperationResult<bool>> ChangePassword(string userId, string oldPassword, string newPassword);
}