using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.AssignUserRoles;

public class AssignUserRolesCommandHandler(ILogger<AssignUserRolesCommandHandler> logger,
    UserManager<User> userManager, RoleManager<IdentityRole> roleManager) : IRequestHandler<AssignUserRolesCommand>
{
    public async Task Handle(AssignUserRolesCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Assigning user roles {@Request}", request);
        var user = await userManager.FindByEmailAsync(request.UserEmail)
            ?? throw new NotFoundException(nameof(User), request.UserEmail.ToString());

        var role = await roleManager.FindByNameAsync(request.RoleName)
            ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName.ToString());

        await userManager.AddToRoleAsync(user, role.Name!);
    }
}
