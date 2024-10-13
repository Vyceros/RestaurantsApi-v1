namespace Restaurants.Application.Restauraunts.RestaurauntPostDtoValidator;

using FluentValidation;
using Restaurants.Application.Restauraunts.Commands.CreateRestauraunt;

public class CreateRestaurauntCommandValidator : AbstractValidator<CreateRestaurauntCommand>
{
    public CreateRestaurauntCommandValidator()
    {
        this.RuleFor(x => x.Name)
            .NotEmpty()
            .Length(8, 50)
            .WithMessage("Name must be between 8 and 50 characters long");
        this.RuleFor(x => x.Description)
            .NotEmpty()
            .Length(8, 100)
            .WithMessage("Description must be between 8 and 100 characters long");

        this.RuleFor(x => x.Socials).EmailAddress().WithMessage("Invalid email address");
    }
}
