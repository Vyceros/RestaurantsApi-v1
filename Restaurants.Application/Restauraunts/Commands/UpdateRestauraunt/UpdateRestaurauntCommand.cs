using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Restaurants.Application.Restauraunts.Commands.UpdateRestauraunt
{
    public class UpdateRestaurauntCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool HasDelivery { get; set; }
    }
}
