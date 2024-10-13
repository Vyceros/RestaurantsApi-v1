using MediatR;

namespace Restaurants.Application.MenuItems.Commands.CreateMenuItem
{
    public class CreateMenuItemCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public int RestaurantId { get; set; }
        public int? KiloCalories { get; set; }

    }
}
