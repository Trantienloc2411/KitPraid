using Duende.IdentityServer.Models;
using Microsoft.Extensions.Configuration;
using System.Linq;


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
        new ApiScope("web", "Website"),
        new ApiScope("offline_access", "Offline access")
    ];

    // Define ApiResources so issued access tokens include the proper 'aud' claim
    public static IEnumerable<ApiResource> ApiResources =>
    [
        new ApiResource("api1", "API 1") { Scopes = { "api1" } },
        new ApiResource("api2", "API 2") { Scopes = { "api2" } },
        new ApiResource("web", "Website API") { Scopes = { "web" } }
    ];

    /// <summary>
    /// Get clients configuration with support for dynamic ports from configuration
    /// </summary>
    public static IEnumerable<Client> GetClients(IConfiguration? configuration = null) =>
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
            RefreshTokenUsage = TokenUsage.ReUse,
            SlidingRefreshTokenLifetime = 1296000,
            AccessTokenLifetime = 3600,
            AllowedScopes = { "api2", "openid", "profile", "roles", "api1", "offline_access" }
        },
        new Client
        {
            ClientId = "postman-client",
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            AllowedScopes = { "openid", "profile", "email", "roles", "api1", "offline_access" },  // Changed from "read:products" to "api1"
            AllowedCorsOrigins = {"http://localhost:5173"},
            AllowOfflineAccess = true,
            RefreshTokenUsage = TokenUsage.ReUse,
            SlidingRefreshTokenLifetime = 1296000,
            AccessTokenLifetime = 3600,
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
            AllowedScopes = 
            {
                "openid",
                "profile",
                "email",
                "roles",
                "api1",
                "web",
                "offline_access"   
            },



            PostLogoutRedirectUris = { "http://localhost:3000/" },
            AllowOfflineAccess = true,
            RefreshTokenUsage = TokenUsage.ReUse,
            SlidingRefreshTokenLifetime = 1296000,
            AccessTokenLifetime = 3600,
            AllowedCorsOrigins = { "http://localhost:3000", "http://localhost:8080" },

            RequireClientSecret = true,           // client must provide secret (or use PKCE)
            RequirePkce = true,                   // enforce PKCE (recommended for browser/mobile apps)
            AllowPlainTextPkce = false,           // enforce secure PKCE
        },
        GetReactClient(configuration)
    ];

    /// <summary>
    /// Get react-client configuration with dynamic port support
    /// Supports reading from configuration or using default ports
    /// </summary>
    private static Client GetReactClient(IConfiguration? configuration)
    {
        // Default ports for common scenarios
        var defaultRedirectUris = new List<string>
        {
            "http://localhost:3000/callback",
            "http://localhost:3000/oauth-callback",  // New OAuth callback with PKCE
            "http://localhost:5173/callback",
            "http://localhost:5173/oauth-callback"   // New OAuth callback with PKCE
        };

        var defaultPostLogoutUris = new List<string>
        {
            "http://localhost:3000/",
            "http://localhost:5173/"
        };

        var defaultCorsOrigins = new List<string>
        {
            "http://localhost:3000",
            "http://localhost:5173"
        };

        // Read additional ports from configuration
        if (configuration != null)
        {
            // Read from IdentityServer:ReactClient:RedirectUris array
            var configRedirectUris = configuration.GetSection("IdentityServer:ReactClient:RedirectUris")
                .Get<string[]>();
            if (configRedirectUris != null && configRedirectUris.Length > 0)
            {
                defaultRedirectUris.AddRange(configRedirectUris);
            }

            // Read from IdentityServer:ReactClient:PostLogoutRedirectUris array
            var configPostLogoutUris = configuration.GetSection("IdentityServer:ReactClient:PostLogoutRedirectUris")
                .Get<string[]>();
            if (configPostLogoutUris != null && configPostLogoutUris.Length > 0)
            {
                defaultPostLogoutUris.AddRange(configPostLogoutUris);
            }

            // Read from IdentityServer:ReactClient:AllowedCorsOrigins array
            var configCorsOrigins = configuration.GetSection("IdentityServer:ReactClient:AllowedCorsOrigins")
                .Get<string[]>();
            if (configCorsOrigins != null && configCorsOrigins.Length > 0)
            {
                defaultCorsOrigins.AddRange(configCorsOrigins);
            }

            // Support for single port configuration (for Aspire dynamic ports)
            var dynamicPort = configuration["IdentityServer:ReactClient:Port"];
            if (!string.IsNullOrEmpty(dynamicPort))
            {
                var baseUrl = $"http://localhost:{dynamicPort}";
                defaultRedirectUris.Add($"{baseUrl}/callback");
                defaultPostLogoutUris.Add($"{baseUrl}/");
                defaultCorsOrigins.Add(baseUrl);
            }
        }

        // Remove duplicates
        defaultRedirectUris = defaultRedirectUris.Distinct().ToList();
        defaultPostLogoutUris = defaultPostLogoutUris.Distinct().ToList();
        defaultCorsOrigins = defaultCorsOrigins.Distinct().ToList();

        // Log configuration for debugging (only in development)
#if DEBUG
        System.Diagnostics.Debug.WriteLine($"[IdentityServer Config] React Client RedirectUris: {string.Join(", ", defaultRedirectUris)}");
        System.Diagnostics.Debug.WriteLine($"[IdentityServer Config] React Client CORS Origins: {string.Join(", ", defaultCorsOrigins)}");
#endif

        return new Client
        {
            ClientId = "react-client",
            // No client secret for public client (browser-based)
            RequireClientSecret = false,
            AllowedGrantTypes = GrantTypes.Code,
            RedirectUris = defaultRedirectUris,
            PostLogoutRedirectUris = defaultPostLogoutUris,
            AllowOfflineAccess = true,
            RefreshTokenUsage = TokenUsage.ReUse,
            SlidingRefreshTokenLifetime = 1296000,
            AllowedScopes = { "openid", "profile", "email", "roles", "api1", "web", "offline_access" },
            AllowedCorsOrigins = defaultCorsOrigins,
            RequirePkce = true,                    // Enforce PKCE for security
            AllowPlainTextPkce = false,           // Require S256 code challenge
            AccessTokenLifetime = 3600,           // 1 hour
            IdentityTokenLifetime = 300           // 5 minutes
        };
    }

    /// <summary>
    /// Legacy method for backward compatibility
    /// </summary>
    public static IEnumerable<Client> Clients => GetClients(null);
}