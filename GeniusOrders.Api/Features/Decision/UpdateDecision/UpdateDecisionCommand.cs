using MediatR;


namespace GeniusOrders.Api.Features.UpdateDecision;

public record UpdateDecisionCommand
(
     int Id,
    int DecisionNumber,
        int DecisionYear,
        DateOnly DecisionDate,
        string Content,
        string? Attachment
) : IRequest;
