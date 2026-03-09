using AutoMapper;
using GeniusOrders.Api.Data;
using MediatR;

namespace GeniusOrders.Api.Features.GetByIdQuery;

public class GetDecisionByIdQueryHandler(GeniusDbContext context, IMapper mapper) : IRequestHandler<GetDecisionByIdQuery, GetDecisionDto>
{
    public async Task<GetDecisionDto> Handle(GetDecisionByIdQuery request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Handling GetDecisionByIdQuery for Id: {request.id}");
        var result = await context.Decisions.FindAsync(request.id);
        var i = mapper.Map<GetDecisionDto>(result);

        return i;
    }

}