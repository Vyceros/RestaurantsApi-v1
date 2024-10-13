using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.IRepositories;


namespace Restaurants.Application.MenuItems.Commands.CreateMenuItem
{
    public class CreateMenuItemCommandHandler(ILogger<CreateMenuItemCommandHandler> logger,
        IRestaurauntsRepository restaurauntsRepository, IMenuItemsRepository menuRepository, IMapper mapper) : IRequestHandler<CreateMenuItemCommand, int>
    {
        public async Task<int> Handle(CreateMenuItemCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating menu item {@MenuItem} for", request);
            var restaurant = await restaurauntsRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }

            var menuItem = mapper.Map<MenuItem>(request);

            int id = await menuRepository.Create(menuItem);

            return id;
        }
    }
}
