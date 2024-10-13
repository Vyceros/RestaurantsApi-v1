using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces.RestaurantAuthorization;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Restauraunts.Commands.UpdateRestauraunt
{
    public class UpdateRestaurauntCommandHandler(
        ILogger<UpdateRestaurauntCommandHandler> logger,
        IRestaurauntsRepository restaurauntsRepository,
        IMapper mapper,
        IRestaurantAuthorizationService authorizationService
    ) : IRequestHandler<UpdateRestaurauntCommand>
    {
        public async Task Handle(
            UpdateRestaurauntCommand request,
            CancellationToken cancellationToken
        )
        {
            logger.LogInformation("Updating restaurant with the id " + "{RestaurauntId} with {@Restauraunt}", request.Id,request);
            var restaurant = await restaurauntsRepository.GetByIdAsync(request.Id);
            if (restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            }
            if (!authorizationService.Authorize(restaurant, ResourceOperation.Update))
            {
                throw new ForbidException();
            }
            mapper.Map(request, restaurant);

            await restaurauntsRepository.SaveChanges();
        }
    }
}
