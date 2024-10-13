using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization.Requirements.MinimumAge;

public class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger,
    IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var user = userContext.GetCurrentUser();

        logger.LogInformation("User : {Email} date of Birth : {DoB} - handling MinimumAgeRequirement",
            user.Email, user.DateOfBirth);

        if (user.DateOfBirth == null)
        {
            logger.LogInformation("User Date of Birth is null");
            context.Fail();
            return Task.CompletedTask;
        }
        if (user.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
        {
            logger.LogInformation("Authorization Success");
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
        return Task.CompletedTask;
    }
}
