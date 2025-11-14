using Duende.IdentityServer.Models;

namespace IdentityServer.Infrastructure.Mapping;

using DomainClient = Domain.Entities.Client;
using IdentityServerClient = Client;
public static class ClientMapper
{
    public static IdentityServerClient ToIdentityServerClient(this DomainClient domainClient)
    {
        return new IdentityServerClient
        {
            ClientId = domainClient.ClientId,
            ClientName = domainClient.ClientName,
            AllowedGrantTypes = domainClient.AllowedGrantTypes,
            AllowedScopes = domainClient.AllowedScopes,
            RedirectUris = domainClient.RedirectUris,
            RequirePkce = domainClient.RequirePkce,
            ClientSecrets = domainClient.ClientSecrets.Select(cs => new Secret(cs.Value.Sha256())
            {
                Type = cs.Type,
                Description = cs.Description,
                Expiration = cs.Expiration
            }).ToList(),
            Enabled = domainClient.IsActive
        };
    }

    public static DomainClient ToDomainClient(this IdentityServerClient model)
    {
        return new DomainClient
        {
            ClientId = model.ClientId,
            ClientName = model.ClientName,
            Description = model.Description,
            AllowedGrantTypes = model.AllowedGrantTypes.ToList(),
            AllowedScopes = model.AllowedScopes.ToList(),
            RedirectUris = model.RedirectUris.ToList(),
            RequirePkce = model.RequirePkce,
            ClientSecrets = model.ClientSecrets.Select(cs => new Domain.Entities.ClientSecret
            {
                Value = cs.Value,
                Type = cs.Type,
                Description = cs.Description,
                Expiration = cs.Expiration
            }).ToList(),
            IsActive = model.Enabled
        };
    }
}