
using System.ComponentModel.DataAnnotations;

namespace Restaurants.Domain.Entities;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
}