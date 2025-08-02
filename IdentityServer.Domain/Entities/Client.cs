using Microsoft.AspNetCore.DataProtection;

namespace IdentityServer.Domain.Entities;

public class Client
{
    public string ClientId { get; set; } 
    public string? ClientName { get; set; }
    public string? Description { get; set; }
    public List<string> AllowedGrantTypes { get; set; } = [];
    public List<string> AllowedScopes { get; set; } = [];
    public List<string> RedirectUris { get; set; } = [];
    public bool RequirePkce { get; set; }
    public List<ClientSecret> ClientSecrets { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}