using Restaurants.Domain.Entities;

namespace Restaurants.Domain.IRepositories;

public interface IMenuItemsRepository
{
    Task<int> Create(MenuItem item);
    Task Delete(MenuItem item);
}
