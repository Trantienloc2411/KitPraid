using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductService.Api.Exceptions;
using ProductService.Api.Test.Controllers;

namespace ProductService.Api.Test
{
    public class TestApplication
    {
        public static WebApplication CreateApp()
        {
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions
            {
                EnvironmentName = Environments.Development
            });

            // IMPORTANT
            builder.WebHost.UseTestServer(); 

            // ---- COPY Program.cs service registrations here ----
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", p => p.RequireRole("Admin"));
                options.AddPolicy("User", p => p.RequireRole("User"));
                options.AddPolicy("Anonymous",
                    p => p.RequireAssertion(c => !c.User.Identity!.IsAuthenticated));
                options.AddPolicy("Customer", p => p.RequireRole("Customer"));
            });

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.Audience = "api1";
                    options.RequireHttpsMetadata = false;
                });

            builder.Services.AddControllers();
            builder.Services.AddLogging();
            builder.Services.AddControllers().AddApplicationPart(typeof(TestErrorController).Assembly);


            var app = builder.Build();

            // ---- COPY Program.cs middleware here ----
            app.UseMiddleware<GlobalExceptionHandler>();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            return app;
        }
    }
}