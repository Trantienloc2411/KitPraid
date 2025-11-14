
using IdentityServer.Application.Services;
using IdentityServer.Domain.Entities;
using IdentityServer.Domain.Repositories;
using IdentityServer.Infrastructure.Configuration;
using IdentityServer.Infrastructure.Data;
using IdentityServer.Infrastructure.Repositories;
using IdentityServer.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DbContext = IdentityServer.Infrastructure.Data.DbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Configure DbContext
builder.Services.AddDbContext<DbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configure Identity
// Note: AddIdentity() automatically registers the Identity.Application scheme
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Configure login path for IdentityServer redirects
    options.SignIn.RequireConfirmedEmail = false;
})
    .AddEntityFrameworkStores<DbContext>()
    .AddDefaultTokenProviders();

// Configure Identity cookie options for login redirect
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

// Configure IdentityServer
// AddAspNetIdentity() will integrate with the Identity scheme already registered above
var identityServerBuilder = builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;

    // Set the issuer URI (important for token validation)
    options.IssuerUri = builder.Configuration["IdentityServer:IssuerUri"] ?? "https://localhost:7070";

    // Configure UserInteraction options to use Razor Pages
    options.UserInteraction.LoginUrl = "/Account/Login";
    options.UserInteraction.LogoutUrl = "/Account/Logout";
    options.UserInteraction.ErrorUrl = "/Error";
})
    .AddDeveloperSigningCredential()
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryClients(Config.GetClients(builder.Configuration)) // Pass configuration for dynamic ports
    .AddAspNetIdentity<ApplicationUser>()
    .AddProfileService<CustomProfileService>();

// In Development, use custom validator that allows any localhost port
// This is helpful when using Aspire with dynamic port assignment
if (builder.Environment.IsDevelopment())
{
    identityServerBuilder.AddRedirectUriValidator<IdentityServer.Infrastructure.Validation.LocalhostRedirectUriValidator>();
}

// Note: Do NOT call AddAuthentication().AddIdentityCookies() here
// because AddIdentity() already registers the Identity.Application scheme
// and AddAspNetIdentity() integrates with it automatically

// Register Application Services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserService, UserService>();

// Register Repositories
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Configure CORS for React client with dynamic port support
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactClient", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            // In Development: Allow any localhost port (for Aspire dynamic ports)
            policy.SetIsOriginAllowed(origin =>
            {
                if (Uri.TryCreate(origin, UriKind.Absolute, out var uri))
                {
                    return uri.Host == "localhost" || uri.Host == "127.0.0.1";
                }
                return false;
            })
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        }
        else
        {
            // In Production: Use specific origins from configuration
            var corsOrigins = new List<string> { "http://localhost:3000", "http://localhost:5173" };

            var dynamicPort = builder.Configuration["IdentityServer:ReactClient:Port"];
            if (!string.IsNullOrEmpty(dynamicPort))
            {
                corsOrigins.Add($"http://localhost:{dynamicPort}");
            }

            var additionalOrigins = builder.Configuration.GetSection("IdentityServer:ReactClient:AllowedCorsOrigins")
                .Get<string[]>();
            if (additionalOrigins != null)
            {
                corsOrigins.AddRange(additionalOrigins);
            }

            policy.WithOrigins(corsOrigins.Distinct().ToArray())
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowReactClient");

// IdentityServer must come before UseAuthentication
app.UseIdentityServer();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
