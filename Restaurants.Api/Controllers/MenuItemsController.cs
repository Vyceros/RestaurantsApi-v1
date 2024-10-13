using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Restaurants.Application.MenuItems;
using Restaurants.Application.MenuItems.Commands.CreateMenuItem;
using Restaurants.Application.MenuItems.Commands.DeleteMenuItem;
using Restaurants.Application.MenuItems.Commands.UpdateMenuItem;
using Restaurants.Application.MenuItems.Queries.GetAllMenuItems;
using Restaurants.Application.MenuItems.Queries.GetMenuItemById;
using Restaurants.Domain.Constants;

namespace Restaurants.Api.Controllers;

// The {restaurantId} parameter in the Route MUST match the FromRoute value provided in the action method
// To correctly bind the route parameter
[Route("api/restauraunts/{restaurantId}/menuItems")]
[ApiController]
[Authorize]
public class MenuItemsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateMenuItem([FromRoute] int restaurantId, CreateMenuItemCommand command)
    {
        command.RestaurantId = restaurantId;

        var menuItemId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetMenuItemById),new { restaurantId, menuItemId },null);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MenuItemDTO>>> GetMenuItems([FromRoute] int restaurantId)
    {
        var menuItems = await mediator.Send(new GetMenuItemsQuery(restaurantId));
        return Ok(menuItems);
    }

    [HttpGet]
    [Route("{menuItemId}")]
    public async Task<IActionResult> GetMenuItemById([FromRoute] int restaurantId, [FromRoute] int menuItemId)
    {

        var menuItem = await mediator.Send(new GetMenuItemByIdQuery(restaurantId, menuItemId));

        return Ok(menuItem);
    }
    [HttpDelete]
    [Authorize(Roles = UserRoles.Owner)]
    [Route("{menuItemId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteMenuItem([FromRoute] int restaurantId, [FromRoute] int menuItemId)
    {
        await mediator.Send(new DeleteMenuItemCommand(restaurantId, menuItemId));

        return NoContent();
    }
    [HttpPatch("{menuItemId}")]
    [Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> UpdateMenuItem([FromRoute] int restaurantId, [FromRoute] int menuItemId,
        UpdateMenuItemCommand command)
    {
        command.RestaurantId = restaurantId;
        command.Id = menuItemId;

        await mediator.Send(command);
        return NoContent();

    }
}
