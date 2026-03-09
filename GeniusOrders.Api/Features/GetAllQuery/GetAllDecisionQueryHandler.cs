using AutoMapper;
using GeniusOrders.Api.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GeniusOrders.Api.Features.GetAllQuery;

public class GetAllDecisionQueryHandler(GeniusDbContext context, IMapper mapper) : IRequestHandler<GetAllDecisionQuery, IEnumerable<GetAllDecisionDto>>
{
    public async Task<IEnumerable<GetAllDecisionDto>> Handle(GetAllDecisionQuery request, CancellationToken cancellationToken)
    {
        var result = await context.Decisions.AsNoTracking().OrderByDescending(d => d.DecisionYear)
                                            .ThenByDescending(d => d.DecisionNumber)
                                            .ToListAsync();

        var i = mapper.Map<IEnumerable<GetAllDecisionDto>>(result);
        return i;
    }
}