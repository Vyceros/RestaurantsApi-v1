using MediatR;
using Restaurants.Application.Restauraunts.DTOs;

namespace Restaurants.Application.Restauraunts.Queries.GetRestaurauntById;

public class GetRestaurauntByIdQuery(int id) : IRequest<RestaurauntDTO>
{
    public int Id { get; } = id;
}
