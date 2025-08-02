using IdentityServer.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Infrastructure.Data;

public class DbContext : IdentityDbContext<ApplicationUser>
{
    public DbContext(DbContextOptions<DbContext> options) :  base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUsers");
        modelBuilder.Entity<ApplicationUser>().HasKey(x => x.Id);
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(x => x.FirstName).HasMaxLength(100);
            entity.Property(x => x.LastName).HasMaxLength(100);
            
        });
    }
}