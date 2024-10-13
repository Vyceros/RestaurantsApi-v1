// Purpose: Contains query for getting all restauraunts.
namespace Restaurants.Application.Restauraunts.Queries.GetAllRestauraunts;

using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.Restauraunts.DTOs;
using Restaurants.Domain.Constants;

public class GetAllRestaurauntsQuery : IRequest<PagedResult<RestaurauntDTO>>
{
    public string? SearchQuery { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set;}
    public string? SortBy { get; set; }
    public SortDirection sortDirection { get; set; }
}
