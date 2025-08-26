using IdentityServer.Domain.Entities;
using IdentityServer.Infrastructure.Configuration;
using IdentityServer.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DbContext = IdentityServer.Infrastructure.Data.DbContext;

namespace IdentityServer.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

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

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
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

        builder.Services.AddControllers();
        
        builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:5001";
                options.Audience = "api1";
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    RoleClaimType = "role"
                };
            });
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
        
        app.UseRouting();
        app.UseCors("AllowFrontEnd");
        app.UseIdentityServer();
        app.UseAuthorization();
        app.MapControllers();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("AllowFrontEnd");
        }

        app.UseHttpsRedirection();
        app.Run();
    }
}