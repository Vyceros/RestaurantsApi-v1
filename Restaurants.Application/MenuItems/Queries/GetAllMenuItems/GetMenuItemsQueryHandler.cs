using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.MenuItems.Queries.GetAllMenuItems;
public class GetMenuItemsQueryHandler(ILogger<GetMenuItemsQueryHandler> logger,
    IRestaurauntsRepository repository,IMapper mapper) : IRequestHandler<GetMenuItemsQuery, IEnumerable<MenuItemDTO>>
{
    public async Task<IEnumerable<MenuItemDTO>> Handle(GetMenuItemsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching all menu items for restauraunt with id {RestaurantId}", request.Id);
        var restaurant = await repository.GetByIdAsync(request.Id);
        if (restaurant is null) { throw new NotFoundException(nameof(Restaurant), request.Id.ToString()); }

        var menuItems = mapper.Map<IEnumerable<MenuItemDTO>>(restaurant.MenuItems);

        return menuItems;
    }
}
