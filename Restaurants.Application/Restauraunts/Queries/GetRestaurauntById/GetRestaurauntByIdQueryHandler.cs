namespace Restaurants.Application.Restauraunts.Queries.GetRestaurauntById;

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restauraunts.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.IRepositories;

public class GetRestaurauntByIdQueryHandler(
    ILogger<GetRestaurauntByIdQueryHandler> logger,
    IRestaurauntsRepository restaurauntRepo,
    IMapper mapper
) : IRequestHandler<GetRestaurauntByIdQuery, RestaurauntDTO>
{
    public async Task<RestaurauntDTO> Handle(
        GetRestaurauntByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Fetching one restauraunt with the id {RestaurauntId}",request.Id);
        var restauraunt = await restaurauntRepo.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        var restaurauntDTO = mapper.Map<RestaurauntDTO>(restauraunt);

        return restaurauntDTO;
    }
}
