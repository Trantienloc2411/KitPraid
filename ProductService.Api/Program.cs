using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ProductService.Api.Exceptions;
using ProductService.Application.Services;
using ProductService.Application.Services.Interfaces;
using ProductService.Domain.Repositories;
using ProductService.Infrastructure.Data;
using ProductService.Infrastructure.Repository;
using Scalar.AspNetCore;

namespace ProductService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
        });
        builder.Services.AddAuthentication("Bearer").
            AddJwtBearer(options =>
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
        builder.Services.AddDbContext<ProductDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<IImageRepository, ImageRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IBrandRepository, BrandRepository>();

        builder.Services.AddScoped<IProductService, Application.Services.ProductService>();
        builder.Services.AddScoped<IBrandService, BrandService>();
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
        
        /*
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new ObjectDictionaryConverter());
        });*/

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
        app.UseAuthentication();
        app.UseAuthorization();

        
        app.MapControllers();
       
        app.Run();
    }
}