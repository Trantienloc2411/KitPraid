using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = DistributedApplication.CreateBuilder(args);

var identityServer = builder.AddProject<Projects.IdentityServer_Api>("identityserver");

builder.Services.AddLogging(logging => logging.AddConsole());

var kitpraid = builder.AddViteApp(
        name: "kitpraid",
        workingDirectory: Path.Combine(Directory.GetCurrentDirectory(), "../KitPraid.FrontEnd/kitpraid"),
        packageManager: "npm")
    .WithReference(identityServer)
    .WithHttpsEndpoint(targetPort: 5173, name: "frontend-http")
    .WithNpmPackageInstallation()
    .WaitFor(identityServer);

builder.Build().Run();