using MediatR;

namespace GeniusOrders.Features.CreateDecision;

public record CreateDecisionComand
(
        int DecisionNumber,
        int DecisionYear,
        DateOnly DecisionDate,
        string Content,
        string? Attachment
) : IRequest<int>;