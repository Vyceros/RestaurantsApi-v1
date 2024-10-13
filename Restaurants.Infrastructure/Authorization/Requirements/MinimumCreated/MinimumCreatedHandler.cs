using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Infrastructure.Authorization.Requirements.MinimumCreated;

public class MinimumCreatedHandler(IUserContext userContext,
    IRestaurauntsRepository restaurauntsRepository) : AuthorizationHandler<MinimumCreated>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumCreated requirement)
    {
        var user = userContext.GetCurrentUser();

        var restaurants = await restaurauntsRepository.GetAllAsync();

        var userOwnedRestaurants = restaurants.Count(r => r.OwnerId == user!.Id);

        if(userOwnedRestaurants >= requirement.MinimumRestaurantsCreated)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }      
    }
}
