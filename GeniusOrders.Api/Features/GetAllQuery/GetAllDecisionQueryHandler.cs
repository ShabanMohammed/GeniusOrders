using AutoMapper;
using GeniusOrders.Api.Data;
using GeniusOrders.Api.Features.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GeniusOrders.Api.Features.GetAllQuery;

public class GetAllDecisionQueryHandler(GeniusDbContext context, IMapper mapper) : IRequestHandler<GetAllDecisionQuery, IEnumerable<DecisionDto>>
{
    public async Task<IEnumerable<DecisionDto>> Handle(GetAllDecisionQuery request, CancellationToken cancellationToken)
    {
        var result = await context.Decisions.AsNoTracking().OrderByDescending(d => d.DecisionYear)
                                            .ThenByDescending(d => d.DecisionNumber)
                                            .ToListAsync();

        var i = mapper.Map<IEnumerable<DecisionDto>>(result);
        return i;
    }
}