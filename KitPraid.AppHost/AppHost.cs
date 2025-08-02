var builder = DistributedApplication.CreateBuilder(args);
var identityServer = 
    builder.AddProject<Projects.IdentityServer_Api>("identityserver")
        ;

builder.Build().Run();