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
        new ApiScope("api2"),
        new ApiScope("customers","Customers"),
        new ApiScope("admins", "Admins"),
        new ApiScope("mobile", "Mobile"),
        new ApiScope("web", "Website")
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

            RedirectUris = { "https://localhost:5001/signin-oidc", "https://localhost:5001/swagger/oauth2-redirect.html" },

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
            AllowedScopes = { "openid", "profile", "email", "roles", "api1" },  // Changed from "read:products" to "api1"
            AllowedCorsOrigins = {"http://localhost:5173"},
            AllowOfflineAccess = true,
        },
        new Client
        {
            ClientId = "external-app",
            ClientSecrets = { new Secret("external-secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
            RedirectUris =
            {
                "http://localhost:3000/callback",
                "http://localhost:3000/oauth2-redirect",
                "http://localhost:8080/callback",
                "capacitor://localhost/callback"
            },


            PostLogoutRedirectUris = { "http://localhost:3000/" },
            AllowOfflineAccess = true,
            AllowedScopes = { "web" },
            AllowedCorsOrigins = { "http://localhost:3000", "http://localhost:8080" },

            RequireClientSecret = true,           // client must provide secret (or use PKCE)
            RequirePkce = true,                   // enforce PKCE (recommended for browser/mobile apps)
            AllowPlainTextPkce = false,           // enforce secure PKCE
        }

    ];
}