using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.MenuItems.Queries.GetMenuItemById;

public class GetMenuItemByIdQueryHandler(ILogger<GetMenuItemByIdQueryHandler> logger,
    IRestaurauntsRepository repository,IMapper mapper) : IRequestHandler<GetMenuItemByIdQuery, MenuItemDTO>
{
    public async Task<MenuItemDTO> Handle(GetMenuItemByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching item with id {MenuItemId} on restaurant {restaurantId}",
            request.MenuItemId,request.RestaurantId);

        var restaurant = await repository.GetByIdAsync(request.RestaurantId);
        if (restaurant == null) { throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString()); }

        var menuItem = restaurant.MenuItems.Find(m => m.Id == request.MenuItemId);
        if (menuItem == null) { throw new NotFoundException(nameof(MenuItem), request.MenuItemId.ToString()); }

        var result = mapper.Map<MenuItemDTO>(menuItem);

        return result;
    }

}
