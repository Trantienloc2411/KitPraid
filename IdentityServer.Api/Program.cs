using IdentityServer.Application.Services;
using IdentityServer.Domain.Entities;
using IdentityServer.Domain.Repositories;
using IdentityServer.Infrastructure.Configuration;
using IdentityServer.Infrastructure.Data;
using IdentityServer.Infrastructure.Repositories;
using IdentityServer.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using DbContext = IdentityServer.Infrastructure.Data.DbContext;

namespace IdentityServer.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // Configure authentication to use JwtBearer as the default scheme for the API
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            // IdentityServer.UI runs on https://localhost:7070
            options.Authority = builder.Configuration["IdentityServer:IssuerUri"] ?? "https://localhost:7070";
            options.Audience = "api1";
            options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                RoleClaimType = "role"
            };
            // Add diagnostic event handlers to log authentication lifecycle events
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("JwtBearer");
                    logger.LogWarning(context.Exception, "JWT authentication failed: {Message}", context.Exception?.Message);
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("JwtBearer");
                    var sub = context.Principal?.FindFirst("sub")?.Value ?? context.Principal?.Identity?.Name;
                    logger.LogInformation("JWT token validated for subject: {Sub}", sub);
                    return Task.CompletedTask;
                },
                OnChallenge = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("JwtBearer");
                    logger.LogWarning("JWT challenge triggered: {Error} - {ErrorDescription}", context.Error, context.ErrorDescription);
                    return Task.CompletedTask;
                }
            };
        });

        // Require JwtBearer for all [Authorize] endpoints by default (prevents cookie auth from being used for API calls)
        builder.Services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
        });
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.SetMinimumLevel(LogLevel.Debug);


        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddControllers();
        builder.Services.AddDbContext<DbContext>(options =>
        {
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<DbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "IdentityServer.Api", Version = "v1" });

            // JWT Bearer (legacy) - keeps the Authorize input for raw tokens
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' followed by space and the token.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            // OAuth2 / OpenID Connect (Authorization Code flow) - integrates with IdentityServer
            var oauthScheme = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri("https://localhost:5001/connect/authorize"),
                        TokenUrl = new Uri("https://localhost:5001/connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "OpenID" },
                            { "profile", "Profile" },
                            { "email", "Email" },
                            { "roles", "Roles" },
                            { "api1", "Api 1" },
                            { "api2", "Api 2" }
                        }
                    }
                }
            };

            c.AddSecurityDefinition("oauth2", oauthScheme);

            // Apply both schemes as possible security requirements
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { oauthScheme, new[] { "openid", "profile", "api1" } },
                { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, new string[] { } }
            });
        });

        builder.Services.AddIdentityServer(option =>
        {
            option.Events.RaiseErrorEvents = true;
            option.Events.RaiseInformationEvents = true;
            option.Events.RaiseFailureEvents = true;
            option.Events.RaiseSuccessEvents = true;
        })
        .AddDeveloperSigningCredential()
        .AddInMemoryIdentityResources(Config.IdentityResources)
        .AddInMemoryApiScopes(Config.ApiScopes)
        .AddInMemoryClients(Config.Clients)
        .AddAspNetIdentity<ApplicationUser>()
        .AddProfileService<CustomProfileService>();

        // Authentication already configured above (JwtBearer)
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontEnd", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                ;

            });
        });

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            await Seeder.SeedRolesAsync(services);
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("AllowFrontEnd");

        app.UseIdentityServer();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityServer.Api v1");
                c.OAuthClientId("interactive");
                // interactive client uses PKCE
                c.OAuthUsePkce();
                c.OAuthAppName("IdentityServer Swagger UI");
                // If you need to pass additional query parameters, you can use c.OAuthConfigObject
            });
        }

        app.Run();
    }
}