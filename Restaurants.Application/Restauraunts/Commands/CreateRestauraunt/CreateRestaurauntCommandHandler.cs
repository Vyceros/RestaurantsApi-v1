using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Restauraunts.Commands.CreateRestauraunt;

public class CreateRestaurauntCommandHandler(
    ILogger<CreateRestaurauntCommandHandler> logger,
    IMapper mapper,
    IRestaurauntsRepository restaurauntRepo,
    IUserContext userContext
) : IRequestHandler<CreateRestaurauntCommand, int>
{
    public async Task<int> Handle(
        CreateRestaurauntCommand request,
        CancellationToken cancellationToken
    )
    {
        var currentUser = userContext.GetCurrentUser();
        logger.LogInformation("{User} is Creating a new restauraunt {@Restauraunt}",currentUser.Email,request);
        var restauraunt = mapper.Map<Restaurant>(request);

        restauraunt.OwnerId = currentUser.Id;

        int id = await restaurauntRepo.Create(restauraunt);
        return id;
    }
}
