using MediatR;

namespace Restaurants.Application.MenuItems.Commands.DeleteMenuItem;

public class DeleteMenuItemCommand(int restaurantId,int menuItemId) : IRequest
{
    public int RestaurantId { get; } = restaurantId;
    public int MenuItemId { get; } = menuItemId;
    
}
