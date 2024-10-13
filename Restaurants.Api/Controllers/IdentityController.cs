using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands.AssignUserRoles;
using Restaurants.Application.Users.Commands.DeleteUserRole;
using Restaurants.Application.Users.Commands.UpdateUser;
using Restaurants.Domain.Constants;

namespace Restaurants.Api.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpPatch("user")]
    [Authorize]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost("userRole")]
    [Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> AssignUserRole(AssignUserRolesCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("userRole")]
    [Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> RemoveUserRole(DeleteUserRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

}
