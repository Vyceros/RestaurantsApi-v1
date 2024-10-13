using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistance;

internal class RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options)
    : IdentityDbContext<User>(options)
{
    internal DbSet<Restaurant> Restaurants { get; set; }
    internal DbSet<MenuItem> MenuItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>().OwnsOne(r => r.Address);

        modelBuilder.Entity<User>()
            .HasMany(r => r.OwnedRestauants)
            .WithOne(o => o.Owner)
            .HasForeignKey(k => k.OwnerId);

        modelBuilder
            .Entity<Restaurant>()
            .HasMany(r => r.MenuItems)
            .WithOne()
            .HasForeignKey(m => m.RestaurantId);
    }
}
