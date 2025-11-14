using IdentityServer.Domain;
using IdentityServer.Domain.Entities;
using IdentityServer.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Infrastructure.Repositories;

public class AuthenticationRepository (SignInManager<ApplicationUser> signInManager) : IAuthenticationRepository
{
    
    public async Task<OperationResult<bool>> LoginAsync(string username, string password)
    {
        var result = await signInManager.PasswordSignInAsync(username, password, false, false);
        if(!result.Succeeded) return OperationResult<bool>
                                .Fail("Login failed! Authentication information incorrect!"); 
        
        return OperationResult<bool>.Ok(true);
    }
}