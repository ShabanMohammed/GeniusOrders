using GeniusOrders.Api.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GeniusOrders.Api.Features.UpdateDecision;

public class UpdateDecisionCommandHandler(GeniusDbContext dbContext) : IRequestHandler<UpdateDecisionCommand>
{
    public async Task Handle(UpdateDecisionCommand request, CancellationToken cancellationToken)
    {
        var decision = await dbContext.Decisions.FirstOrDefaultAsync(d => d.Id == request.Id);

        if (decision is null)
        {
            throw new KeyNotFoundException("القرار غير موجود");
        }

        decision.DecisionNumber = request.DecisionNumber;
        decision.DecisionYear = request.DecisionYear;
        decision.DecisionDate = request.DecisionDate;
        decision.Content = request.Content;
        decision.Attachment = request.Attachment;

        await dbContext.SaveChangesAsync();

    }
}