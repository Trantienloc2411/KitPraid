using CategoryService.Domain.Entities;
using CategoryService.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CategoryService.Infrastructure.Data;

public class CategoryDbContext : DbContext
{
    public CategoryDbContext(DbContextOptions<CategoryDbContext> options) : base(options) {}
    // public CategoryDbContext(DbContextOptions<DbContext> optionsBuilderOptions) {}
    
    public virtual DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Category>().HasKey(e => e.Id);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryDbContext).Assembly);
        
        modelBuilder.Entity<Category>().ToTable("Categories");
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasOne(c => c.ParentCategory)
                .WithMany(c => c.ChildCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Category>()
            .HasIndex(c => c.ParentCategoryId);
        
        CategorySeeding.SeedCategories(modelBuilder);
    }
}