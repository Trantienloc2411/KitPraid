using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = DistributedApplication.CreateBuilder(args);
var identityServer = builder.AddProject<Projects.IdentityServer_Api>("identityserver")
    .WithHttpsEndpoint(port: 5002, name: "identityserver-https");
var gateway = builder.AddProject<Projects.KitPraid_Gateway>("gateway")
    .WithHttpsEndpoint(port: 7215, name: "gateway-https")
    .WithReference(identityServer)
    .WaitFor(identityServer);
var productApi = builder.AddProject<Projects
        .ProductService_Api>("product-api")
    .WithHttpsEndpoint(port: 6162, name: "product-api-https")
    .WaitFor(identityServer);
var categoryApi = builder.AddProject<Projects.CategoryService_Api>("category-api")
    .WithHttpsEndpoint(port: 8386, name: "category-api-https");
    
var kitpraid = builder.AddViteApp(  
        name: "kitpraid",  
        workingDirectory: Path.Combine(Directory.GetCurrentDirectory(), "../KitPraid.FrontEnd/kitpraid"),  
        packageManager: "npm")  
    .WithEnvironment("PORT", "3000") // Set port via environment variable instead
    .WithNpmPackageInstallation()  
    .WaitFor(identityServer)
    .WithHttpEndpoint(port:3000, name:"frontend", isProxied:false);

var identityServerUi = builder.AddProject<Projects.IdentityServer_UI>("identityserver-ui")
    .WithEnvironment("FRONTEND", kitpraid.GetEndpoint("frontend"));
builder.Services.AddLogging(logging => logging.AddConsole());

identityServerUi.WithReference(kitpraid);
builder.Build().Run();