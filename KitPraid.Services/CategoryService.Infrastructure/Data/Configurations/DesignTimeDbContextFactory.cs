using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CategoryService.Infrastructure.Data.Configurations;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CategoryDbContext>
{
    public CategoryDbContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../CategoryService.Api"); // ✅ adjust if needed

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.Development.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<CategoryDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        Console.WriteLine(connectionString);
        optionsBuilder.UseSqlServer(connectionString);

        return new CategoryDbContext(optionsBuilder.Options);
    }
}