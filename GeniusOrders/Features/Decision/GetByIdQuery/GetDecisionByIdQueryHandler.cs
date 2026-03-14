using AutoMapper;
using GeniusOrders.Data;
using GeniusOrders.Client.Dtos;
using MediatR;

namespace GeniusOrders.Features.GetByIdQuery;

public class GetDecisionByIdQueryHandler(GeniusDbContext context, IMapper mapper) : IRequestHandler<GetDecisionByIdQuery, DecisionDto>
{
    public async Task<DecisionDto> Handle(GetDecisionByIdQuery request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Handling GetDecisionByIdQuery for Id: {request.id}");
        var result = await context.Decisions.FindAsync(request.id);
        var i = mapper.Map<DecisionDto>(result);

        return i;
    }

}