using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = DistributedApplication.CreateBuilder(args);
var identityServer = builder.AddProject<Projects.IdentityServer_Api>("identityserver")
    .WithHttpsEndpoint(port: 5002, name: "identityserver-https");
var identityServerUi = builder.AddProject<Projects.IdentityServer_UI>("identityserver-ui")
    .WithHttpsEndpoint(port: 7072, name : "identityserver-ui");
var gateway = builder.AddProject<Projects.KitPraid_Gateway>("gateway")
    .WithHttpsEndpoint(port: 7215, name: "gateway-https")
    .WithReference(identityServer)
    .WaitFor(identityServer);
    
builder.Services.AddLogging(logging => logging.AddConsole());

var kitpraid = builder.AddViteApp(  
        name: "kitpraid",  
        workingDirectory: Path.Combine(Directory.GetCurrentDirectory(), "../KitPraid.FrontEnd/kitpraid"),  
        packageManager: "npm")  
    .WithReference(identityServerUi)
    .WithEnvironment("PORT", "3000") // Set port via environment variable instead
    .WithNpmPackageInstallation()  
    .WaitFor(identityServer);

// Pass FrontEnd URL to IdentityServer.UI for dynamic port configuration
// Note: ViteApp port is dynamic, so we configure it via appsettings.Development.json
// or you can get the actual port from Aspire dashboard and set it manually
identityServerUi.WithReference(kitpraid);

builder.Build().Run();