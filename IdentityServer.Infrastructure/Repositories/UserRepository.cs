using IdentityServer.Domain.Entities;
using IdentityServer.Domain.Repositories;
using IdentityServer.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser?> _userManager;
    private readonly DbContext _dbContext;

    public UserRepository(UserManager<ApplicationUser?> userManager, DbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }
    public async Task<ApplicationUser?> GetByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<ApplicationUser?> GetByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<ApplicationUser?> CreateAsync(ApplicationUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        return result.Succeeded ? await GetByIdAsync(user.Id) : null;
    }

    public async Task<bool> UpdateAsync(ApplicationUser user)
    {
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }

    public async Task<bool> DeleteAsync(string id)
    {
       var user = await GetByIdAsync(id);
       if (user is null) return false;
       var result = await _userManager.DeleteAsync(user);
       return result.Succeeded;
    }

    public async Task<bool> ValidateCredentialsAsync(string email, string password)
    {
        var user = await GetByEmailAsync(email);
        if (user is null || !user.IsActive) return false;
        var result = await _userManager.CheckPasswordAsync(user, password);
        return result;
    }
}