using FluentValidation;
using FluentValidation.AspNetCore;

namespace Restaurants.Application.MenuItems.Commands.CreateMenuItem;

public class CreateMenuItemValidator : AbstractValidator<CreateMenuItemCommand>
{
    public CreateMenuItemValidator()
    {
        RuleFor(m => m.Price).GreaterThanOrEqualTo(0)
            .WithMessage("Price must be a positive number");

        RuleFor(c => c.KiloCalories).GreaterThanOrEqualTo(0)
            .WithMessage("KiloCalories must be a positive number");
    }
}
