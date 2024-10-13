using MediatR;

namespace Restaurants.Application.MenuItems.Commands.UpdateMenuItem;

public class UpdateMenuItemCommand(int restaurantId,int menuItemId) : IRequest
{
    public int Id { get; set; } = menuItemId;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public int RestaurantId { get; set; } = restaurantId;
    public int? KiloCalories { get; set; }
}
