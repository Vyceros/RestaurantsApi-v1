using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements.MinimumCreated;

public class MinimumCreated(int minCreated) : IAuthorizationRequirement
{
    public int MinimumRestaurantsCreated { get; } = minCreated;
}
