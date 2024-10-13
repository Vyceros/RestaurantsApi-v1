using Restaurants.Domain.Entities;
using Restaurants.Domain.IRepositories;
using Restaurants.Infrastructure.Persistance;

namespace Restaurants.Infrastructure.Repositories;

internal class MenuItemsRepository(RestaurantsDbContext dbContext) : IMenuItemsRepository
{
    public async Task<int> Create(MenuItem item)
    {
        dbContext.MenuItems.Add(item);
        await dbContext.SaveChangesAsync();

        return item.Id;
    }

    public async Task Delete(MenuItem item)
    {
        dbContext.MenuItems.Remove(item);
        await dbContext.SaveChangesAsync();
    }
}
