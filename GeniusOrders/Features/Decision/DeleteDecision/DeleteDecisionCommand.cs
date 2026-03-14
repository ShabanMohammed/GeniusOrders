using GeniusOrders.Api.Data;
using MediatR;

namespace GeniusOrders.Features.DeleteDecision;


public record DeleteDecisionCommand(int id) : IRequest;


public class DeleteCommandHandler(GeniusDbContext dbContext) : IRequestHandler<DeleteDecisionCommand>
{


    Task IRequestHandler<DeleteDecisionCommand>.Handle(DeleteDecisionCommand request, CancellationToken cancellationToken)
    {
        var decision = dbContext.Decisions.Find(request.id);
        if (decision is null)
        {
            throw new KeyNotFoundException($"القرار بالمعرف {request.id} غير موجود.");
        }

        dbContext.Decisions.Remove(decision);
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}