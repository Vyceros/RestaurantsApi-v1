using MediatR;

namespace Restaurants.Application.Users.Commands.AssignUserRoles;
public class AssignUserRolesCommand : IRequest
{
    public string UserEmail { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}
