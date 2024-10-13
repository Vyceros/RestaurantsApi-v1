namespace Restaurants.Application.Restauraunts.Commands.DeleteRestauarunt;

using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces.RestaurantAuthorization;
using Restaurants.Domain.IRepositories;

public class DeleteRestaurauntCommandHandler(
    ILogger<DeleteRestaurauntCommandHandler> logger,
    IRestaurauntsRepository restaurauntsRepository,
    IRestaurantAuthorizationService authorizationService
) : IRequestHandler<DeleteRestaurauntCommand>
{
    public async Task Handle(
        DeleteRestaurauntCommand request,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Deleting restauraunt with id {RestaurauntId}",request.Id);
        var restaurant = await restaurauntsRepository.GetByIdAsync(request.Id);

        if (restaurant is null)
        {
            throw new NotFoundException(nameof(Restaurant),request.Id.ToString());
        }
        if (!authorizationService.Authorize(restaurant, ResourceOperation.Delete))
        {
            throw new ForbidException();
        }

        await restaurauntsRepository.Delete(restaurant);
        
    }
}
