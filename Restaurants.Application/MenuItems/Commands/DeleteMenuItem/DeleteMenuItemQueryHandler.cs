using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces.RestaurantAuthorization;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.MenuItems.Commands.DeleteMenuItem;

public class DeleteMenuItemQueryHandler(ILogger<DeleteMenuItemQueryHandler> logger,
    IRestaurauntsRepository repository, IMenuItemsRepository menuRepository,
    IRestaurantAuthorizationService authorizationService) : IRequestHandler<DeleteMenuItemCommand>
{
    public async Task Handle(DeleteMenuItemCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting dish with id :{menuItemId}", request.MenuItemId);

        var restaurant = await repository.GetByIdAsync(request.RestaurantId);
        if (restaurant == null) { throw new NotFoundException(nameof(Restaurant),request.RestaurantId.ToString()); }


        var menuItem = restaurant.MenuItems.Find(r => r.Id == request.MenuItemId);

        if (!authorizationService.Authorize(restaurant, ResourceOperation.Delete))
        {
            throw new ForbidException();
        }
        await menuRepository.Delete(menuItem);

    }
}
