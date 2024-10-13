using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces.RestaurantAuthorization;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.MenuItems.Commands.UpdateMenuItem;

public class UpdateMenuItemCommandHandler(ILogger<UpdateMenuItemCommandHandler> logger,
    IRestaurauntsRepository repository,IMapper mapper,
    IRestaurantAuthorizationService authorizationService) : IRequestHandler<UpdateMenuItemCommand>
{
    public async Task Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating dish with id : {menuId}",request.Id);
        var restaurant = await repository.GetByIdAsync(request.RestaurantId);
        if (restaurant == null) { throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());}

        var menuItem = restaurant.MenuItems.Find(r => r.Id == request.Id);

        if (menuItem == null) { throw new NotFoundException(nameof(MenuItem), request.Id.ToString());}

        if (!authorizationService.Authorize(restaurant, ResourceOperation.Delete))
        {
            throw new ForbidException();
        }

        mapper.Map(request, menuItem);

        await repository.SaveChanges();


    }
}
