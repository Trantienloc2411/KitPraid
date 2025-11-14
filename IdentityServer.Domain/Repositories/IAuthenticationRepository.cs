using IdentityServer.Domain.Entities;

namespace IdentityServer.Domain.Repositories;

public interface IAuthenticationRepository
{
    public Task<OperationResult<bool>> LoginAsync(string username, string password);
}