using FluentValidation;
using Restaurants.Application.Restauraunts.DTOs;

namespace Restaurants.Application.Restauraunts.Queries.GetAllRestauraunts;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurauntsQuery>
{
    private int[] AllowedPageSizes = [5, 10, 15, 30];
    private string[] SortBy = [nameof(RestaurauntDTO.Name), nameof(RestaurauntDTO.Description)];

    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize)
            .Must(value => AllowedPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",",AllowedPageSizes)}]");

        RuleFor(s => s.SortBy)
            .Must(value => SortBy.Contains(value))
            .When(s => s.SortBy != null)
            .WithMessage("Please sort by either Name or Description");

    }
}
