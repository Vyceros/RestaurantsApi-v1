namespace Restaurants.Domain.Entities;

public class Restaurant : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool HasDelivery { get; set; }
    public bool IsOpen { get; set; }

    public string? ContactNumber { get; set; }
    public string? Socials { get; set; }

    public Address? Address { get; set; }

    public List<MenuItem>? MenuItems { get; set; } = new();

    public User Owner { get; set; } = default!;
    public string OwnerId { get; set; } = default!;

}