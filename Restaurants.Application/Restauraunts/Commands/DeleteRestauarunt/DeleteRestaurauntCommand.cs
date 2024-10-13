using MediatR;

namespace Restaurants.Application.Restauraunts.Commands.DeleteRestauarunt;

public class DeleteRestaurauntCommand(int id) : IRequest
{
    public int Id { get; } = id;
}
