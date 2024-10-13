using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.IRepositories;
using Restaurants.Infrastructure.Persistance;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurauntsRepository(RestaurantsDbContext dbContext) : IRestaurauntsRepository
{
    public async Task<int> Create(Restaurant restaurant)
    {
        dbContext.Restaurants.Add(restaurant);
        await dbContext.SaveChangesAsync();
        return restaurant.Id;
    }

    public async Task Delete(Restaurant restaurant)
    {
        dbContext.Remove(restaurant);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restauraunts = await dbContext.Restaurants.Include(r => r.MenuItems).ToListAsync();
        return restauraunts;
    }

    public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchQuery,
        int pageSize,
        int pageNumber,
        string? sortBy,
        SortDirection direction)
    {
        var searchQueryLower = searchQuery?.ToLower();

        var baseQuery = dbContext.Restaurants

            .Where(r => searchQueryLower == null || (r.Name.ToLower().Contains(searchQueryLower)
            || r.Description.ToLower().Contains(searchQueryLower)));

        var totalCount = await baseQuery.CountAsync();

        if(sortBy != null)
        {
            var column = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                {nameof(Restaurant.Name),r => r.Name },
                {nameof(Restaurant.Description),r => r.Description }
            };

            var selectedColumn = column[sortBy];

            baseQuery = direction == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var selectedRestaurants = await baseQuery
            .Skip(pageSize * (pageNumber -1))
            .Take(pageSize)
            .ToListAsync();

        return (selectedRestaurants, totalCount);
    }

    public async Task<Restaurant> GetByIdAsync(int id)
    {
        try
        {
            var restauraunt = await dbContext.Restaurants
                .Include(r => r.MenuItems)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restauraunt is null)
            {
                return null;
            }
            return restauraunt;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();
}
