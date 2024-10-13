using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistance;

namespace Restaurants.Infrastructure.Seeders;

internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Restaurants.Any())
            {
                var restaurants = GetRestauraunts();
                dbContext.Restaurants.AddRange(restaurants);
                await dbContext.SaveChangesAsync();
            }

            if(!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles =
            [
                new(UserRoles.User)
                {
                    NormalizedName = UserRoles.User.ToUpper()
                },

                new(UserRoles.Admin)
                {
                    NormalizedName = UserRoles.Admin.ToUpper()
                },

                new(UserRoles.Owner)
                {
                    NormalizedName = UserRoles.Owner.ToUpper()
                }
            ];
        return roles;
    }
    private IEnumerable<Restaurant> GetRestauraunts()
    {
        List<Restaurant> restaurants = [
            new()
            {
                Name = "Restaurant 1",
                Description = "Description 1",
                HasDelivery = true,
                IsOpen = true,
                Socials = "Socials 1",

                Address =
                 new(){
                    City = "City 1",
                    Street = "Street 1",
                    PostalCode = "ZipCode 1"
                 },
                 MenuItems = [
                    new()
                    {
                        Name = "Menu Item 1",
                        Description = "Description 1",
                        Price = 10.00m
                    },
                    new()
                    {
                        Name = "Menu Item 2",
                        Description = "Description 2",
                        Price = 20.00m
                    },
                    new()
                    {
                        Name = "Menu Item 3",
                        Description = "Description 3",
                        Price = 30.00m
                    }
                 ]
            },
            new Restaurant()
            {
                Name = "Restaurant 2",
                Description = "Description 2",
                HasDelivery = false,
                IsOpen = false,

                ContactNumber = "Contact Number 2",
                Socials = "Socials 2",
                Address = new()
                {
                    City = "City 2",
                    Street = "Street 2",
                    PostalCode = "ZipCode 2"
                },
            }
        ];
        return restaurants;
    }
}