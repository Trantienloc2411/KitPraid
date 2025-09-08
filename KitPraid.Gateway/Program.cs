namespace KitPraid.Gateway;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
       
        builder.Services.AddReverseProxy()
            .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

        builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = builder.Configuration["Authority"];
                options.TokenValidationParameters.ValidateAudience = false;
            });
        
        builder.Services.AddAuthorization();
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapReverseProxy();
        app.UseHttpsRedirection();

        app.UseAuthorization();

      
        app.Run();
    }
}