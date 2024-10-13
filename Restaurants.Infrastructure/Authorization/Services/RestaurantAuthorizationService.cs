using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces.RestaurantAuthorization;

namespace Restaurants.Infrastructure.Authorization.Services;
public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger,
    IUserContext userContext) : IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("Authorizing user {UserEmail}, to {OperationName} for restaurant {RestaurantName}"
            , currentUser.Email,
            resourceOperation, restaurant.Name);

        if (resourceOperation == ResourceOperation.Create || resourceOperation == ResourceOperation.Read)
        {

            logger.LogInformation("Create/Read operation, authorization successful");
            return true;
        }

        if (resourceOperation == ResourceOperation.Delete && currentUser.IsInRole(UserRoles.Admin))
        {
            logger.LogInformation("Authorizing admin user for delete operation");
            return true;
        }
        if ((resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update)
            && currentUser.Id == restaurant.OwnerId)
        {
            logger.LogInformation("Owner user updating restaurant, authorization successful.");
            return true;
        }
        return false;
    }
}
