using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Restaurants.Application.Restauraunts.Commands.UpdateRestauraunt
{
    public class UpdateRestaurauntCommandValidator : AbstractValidator<UpdateRestaurauntCommand>
    {
        public UpdateRestaurauntCommandValidator()
        {
            this.RuleFor(x => x.Name)
                .NotEmpty()
                .Length(8, 50)
                .WithMessage("Name must be between 8 and 50 characters long");
            this.RuleFor(x => x.Description)
                .NotEmpty()
                .Length(8, 100)
                .WithMessage("Description must be between 8 and 100 characters long");
            RuleFor(x => x.HasDelivery).NotNull().WithMessage("HasDelivery must be provided");
        }
    }
}
