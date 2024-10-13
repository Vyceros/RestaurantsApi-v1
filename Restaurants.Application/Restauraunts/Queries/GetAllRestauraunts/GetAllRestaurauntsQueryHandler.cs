using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Restauraunts.DTOs;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Restauraunts.Queries.GetAllRestauraunts;

public class GetAllRestaurauntsQueryHandler(
    ILogger<GetAllRestaurauntsQueryHandler> logger,
    IRestaurauntsRepository restaurauntRepo,
    IMapper mapper
) : IRequestHandler<GetAllRestaurauntsQuery, PagedResult<RestaurauntDTO>>
{
    public async Task<PagedResult<RestaurauntDTO>> Handle(
        GetAllRestaurauntsQuery request,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Getting all restauraunts");
        var (restauraunts , totalCount) = await restaurauntRepo.GetAllMatchingAsync(request.SearchQuery,
            request.PageSize,
            request.PageNumber,
            request.SortBy,
            request.sortDirection);

        var restaurauntsDTO = mapper.Map<IEnumerable<RestaurauntDTO>>(restauraunts);

        
        var pagedResult = new PagedResult<RestaurauntDTO>(restaurauntsDTO, totalCount, request.PageSize, request.PageNumber);
        return pagedResult;
    }
}
