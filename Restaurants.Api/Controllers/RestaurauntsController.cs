using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restauraunts.Commands.CreateRestauraunt;
using Restaurants.Application.Restauraunts.Commands.DeleteRestauarunt;
using Restaurants.Application.Restauraunts.Commands.UpdateRestauraunt;
using Restaurants.Application.Restauraunts.DTOs;
using Restaurants.Application.Restauraunts.Queries.GetAllRestauraunts;
using Restaurants.Application.Restauraunts.Queries.GetRestaurauntById;
using Restaurants.Domain.Constants;

namespace Restaurants.Api.Controllers;

[ApiController]
[Route("api/restaurants")]
[Authorize]
public class RestaurauntsController : ControllerBase
{
    private readonly IMediator mediator;

    public RestaurauntsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(200,Type = typeof(IEnumerable<RestaurauntDTO>))]
    [AllowAnonymous]
    public async Task<IActionResult> GetRestauraunts([FromQuery] GetAllRestaurauntsQuery query)
    {
        var restauraunts = await mediator.Send(query);
        return Ok(restauraunts);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(RestaurauntDTO))]
    public async Task<IActionResult> GetRestaurantById([FromRoute] int id)
    {
        var restauraunt = await mediator.Send(new GetRestaurauntByIdQuery(id));
        return Ok(restauraunt);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
    {
        await mediator.Send(new DeleteRestaurauntCommand(id));
       
         return NoContent();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> UpdateRestaurant(
        [FromRoute] int id,
        UpdateRestaurauntCommand command
    )
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant(CreateRestaurauntCommand command)
    {
        int id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetRestaurantById), new { id }, null);
    }
}
