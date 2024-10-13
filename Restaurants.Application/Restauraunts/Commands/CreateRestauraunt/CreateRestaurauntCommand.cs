using MediatR;

namespace Restaurants.Application.Restauraunts.Commands.CreateRestauraunt;

public class CreateRestaurauntCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool HasDelivery { get; set; }

    public bool IsOpen { get; set; }

    public string? ContactNumber { get; set; }

    public string? Socials { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public string? PostalCode { get; set; }
}
