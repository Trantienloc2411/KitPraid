using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;

namespace ProductService.Infrastructure.Data;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<DbContext> optionsBuilderOptions) {}
    
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) {}
    
    public virtual DbSet<Brand> Brands { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Image>  Images { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>().HasKey(e => e.Id);
        modelBuilder.Entity<Product>().HasKey(e => e.Id);
        modelBuilder.Entity<Image>().HasKey(e => e.Id);

        modelBuilder.Entity<Product>().HasOne(e => e.Brand).WithMany(e => e.Products).HasForeignKey(e => e.BrandId);
        modelBuilder.Entity<Product>().HasMany(e => e.Images).WithOne(e => e.Product).HasForeignKey(e => e.ProductId);
        
        
        modelBuilder.Entity<Product>().Ignore(e => e.Attributes);
        
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
    }
    
}