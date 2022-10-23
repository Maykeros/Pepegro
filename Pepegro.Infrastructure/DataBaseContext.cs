namespace Infrastructure;

using Authentication;
using Domain.Entities.Authorization;
using Domain.Entities.MainEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class DataBaseContext : IdentityDbContext<User, Role, int>
{
    public DataBaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new RoleConfiguration());
            
        
        base.OnModelCreating(builder);
        builder.Entity<Order>().HasKey(O => O.Id);

        builder.Entity<Order>()
            .HasOne(o => o.Product)
            .WithMany(p => p.Orders)
            .HasForeignKey(o => o.ProductId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}