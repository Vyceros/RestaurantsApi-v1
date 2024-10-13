using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.DeleteUserRole;

public class DeleteUserRoleCommandHandler(ILogger<DeleteUserRoleCommandHandler> logger,
    RoleManager<IdentityRole> roleManager,UserManager<User> userManager) : IRequestHandler<DeleteUserRoleCommand>
{
    public async Task Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Removing role {@Request}",request.RoleName);

        var user = await userManager.FindByEmailAsync(request.UserEmail)
             ?? throw new NotFoundException(nameof(User), request.UserEmail.ToString());

        var role = await roleManager.FindByNameAsync(request.RoleName)
            ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName.ToString());

        await userManager.RemoveFromRoleAsync(user, role.Name!);
    }
}
