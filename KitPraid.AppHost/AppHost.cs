var builder = DistributedApplication.CreateBuilder(args);
<<<<<<< Updated upstream
var identityServer = 
    builder.AddProject<Projects.IdentityServer_Api>("identityserver")
        ;
=======

var identityServer = builder.AddProject<Projects.IdentityServer_Api>("identityserver");
var gateway = builder.AddProject<Projects.IdentityServer_Api>("gateway");
builder.Services.AddLogging(logging => logging.AddConsole());

var kitpraid = builder.AddViteApp(
        name: "kitpraid",
        workingDirectory: Path.Combine(Directory.GetCurrentDirectory(), "../KitPraid.FrontEnd/kitpraid"),
        packageManager: "npm")
    .WithReference(identityServer)
    .WithHttpsEndpoint(targetPort: 5173, name: "frontend-http")
    .WithNpmPackageInstallation()
    .WaitFor(identityServer);
>>>>>>> Stashed changes

builder.Build().Run();