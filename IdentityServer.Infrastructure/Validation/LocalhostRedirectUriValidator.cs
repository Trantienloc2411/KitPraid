using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace IdentityServer.Infrastructure.Validation;

/// <summary>
/// Custom Redirect URI validator that allows any localhost port in Development environment
/// This is useful when using Aspire with dynamic port assignment
/// </summary>
public class LocalhostRedirectUriValidator : IRedirectUriValidator
{
    private readonly bool _isDevelopment;

    public LocalhostRedirectUriValidator(IWebHostEnvironment environment)
    {
        _isDevelopment = environment.IsDevelopment();
    }

    public Task<bool> IsRedirectUriValidAsync(string requestedUri, Client client)
    {
        // In development, allow any localhost port for react-client
        if (_isDevelopment && client.ClientId == "react-client")
        {
            if (Uri.TryCreate(requestedUri, UriKind.Absolute, out var uri))
            {
                // Allow any localhost port with /callback path
                if (uri.Host == "localhost" && 
                    (uri.Scheme == "http" || uri.Scheme == "https") &&
                    uri.AbsolutePath == "/callback")
                {
                    return Task.FromResult(true);
                }
            }
        }

        // Fallback to default validation: check if URI is in AllowedRedirectUris
        var isValid = client.RedirectUris.Contains(requestedUri);
        return Task.FromResult(isValid);
    }

    public Task<bool> IsPostLogoutRedirectUriValidAsync(string requestedUri, Client client)
    {
        // In development, allow any localhost port for react-client
        if (_isDevelopment && client.ClientId == "react-client")
        {
            if (Uri.TryCreate(requestedUri, UriKind.Absolute, out var uri))
            {
                // Allow any localhost port with root path
                if (uri.Host == "localhost" && 
                    (uri.Scheme == "http" || uri.Scheme == "https") &&
                    uri.AbsolutePath == "/")
                {
                    return Task.FromResult(true);
                }
            }
        }

        // Fallback to default validation
        var isValid = client.PostLogoutRedirectUris.Contains(requestedUri);
        return Task.FromResult(isValid);
    }
}

