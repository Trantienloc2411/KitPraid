using System.Text.Json.Serialization;
using CategoryService.Application.Services.Interfaces;
using CategoryService.Domain.Repositories;
using CategoryService.Infrastructure.Data;
using CategoryService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace CategoryService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorizationBuilder()
            // Add services to the container.
            .AddPolicy("Admin", policy => policy.RequireRole("Admin"));
        builder.Services.AddAuthorizationBuilder()
            .AddPolicy("User", policy => policy.RequireRole("User"));
        builder.Services.AddAuthorizationBuilder()
            .AddPolicy("Anonymous",
                policy => policy.RequireAssertion(context => !context.User.Identity.IsAuthenticated));

        builder.Services.AddAuthorizationBuilder()
            .AddPolicy("Customer", policy => policy.RequireRole("Customer"));
        builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration["Authority"];
                options.Audience = "api1";

                options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();

                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.
                    TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuers = new[]
                        {
                            "https://localhost:5001",
                            "https://localhost:5002",
                            "https://localhost:7072"
                        },
                        ValidateAudience = false
                    };
            }
        );
        

        
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddScoped<ICategoryService, Application.Services.CategoryService>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddDbContext<CategoryDbContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseHttpsRedirection();
        app.UseMiddleware<GlobalExceptionHandler>();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();

        

        app.Run();
    }
}