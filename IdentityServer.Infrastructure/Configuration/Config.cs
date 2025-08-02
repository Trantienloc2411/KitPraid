using Duende.IdentityServer.Models;


namespace IdentityServer.Infrastructure.Configuration;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Email(),
        new IdentityResource("roles","User roles", new[] {"roles"})
    ];

    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new ApiScope("api1", "Api1"),
        new ApiScope("api2")
    ];

    public static IEnumerable<Client> Clients =>
    [
        new Client
        {
            ClientId = "client",
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes = { "api1" }
        },
        
        new Client
        {
            ClientId = "interactive",
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.Code,
            
            RedirectUris = { "https://localhost:5001/signin-oidc" },
            
            FrontChannelLogoutUri = "https://localhost:5001/signout-oidc",
            PostLogoutRedirectUris = { "https://localhost:5001/" },
            
            AllowOfflineAccess = true,
            AllowedScopes = { "api2", "openid", "profile", "roles", "api1" }
        },
        new Client
        {
            ClientId = "postman-client",
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            AllowedScopes = { "openid", "profile", "email", "roles", "read:products" }
        }

    ];
}