using MediatR;

namespace Restaurants.Application.MenuItems.Queries.GetMenuItemById;

public class GetMenuItemByIdQuery(int restaurantId,int menuId) : IRequest<MenuItemDTO>
{
    public int RestaurantId { get; } = restaurantId;
    public int MenuItemId { get; } = menuId;
}
