using AutoMapper;
using GeniusOrders.Api.Data;
using MediatR;
using GeniusOrders.Api.Entities;
using GeniusOrders.Api.Features.CreateDecision;

namespace GeniusOrders.Api.Features.CreateDecision;

public class CreateDecisionComandHandler(GeniusDbContext dbContext) : IRequestHandler<CreateDecisionComand, int>
{
    public async Task<int> Handle(CreateDecisionComand request, CancellationToken cancellationToken)
    {
        var decision = new Decision
        {
            DecisionNumber = request.DecisionNumber,
            DecisionYear = request.DecisionYear,
            DecisionDate = request.DecisionDate,
            Content = request.Content
        };

        dbContext.Decisions.Add(decision);
        await dbContext.SaveChangesAsync(cancellationToken);
        return decision.Id;

    }
}