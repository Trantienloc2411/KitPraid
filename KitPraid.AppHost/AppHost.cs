using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = DistributedApplication.CreateBuilder(args);
var identityServer = builder.AddProject<Projects.IdentityServer_Api>("identityserver")
    .WithHttpsEndpoint(port: 5001, name: "identityserver-https");
var identityServerUi = builder.AddProject<Projects.IdentityServer_UI>("identityserver-ui")
    .WithEndpoint(port : 8386, name : "identityserver-ui-ui");
var gateway = builder.AddProject<Projects.KitPraid_Gateway>("gateway")
    .WithHttpsEndpoint(port: 7214, name: "gateway-https")
    .WithReference(identityServer);
    
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