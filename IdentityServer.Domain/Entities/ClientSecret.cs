namespace IdentityServer.Domain.Entities;

public class ClientSecret
{
    public string? Value { get; set; }
    public string Type { get; set; } = "SharedSecret";
    public string? Description { get; set; }
    public DateTime? Expiration { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
}