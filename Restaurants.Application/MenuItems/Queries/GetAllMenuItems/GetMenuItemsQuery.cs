using MediatR;

namespace Restaurants.Application.MenuItems.Queries.GetAllMenuItems;

public class GetMenuItemsQuery(int restaurantId) : IRequest<IEnumerable<MenuItemDTO>>
{
    public int Id { get; } = restaurantId;
}
