namespace GeniusOrders.Api.Features.UpdateDecision;

public record UpdateDecisionDto
{
    public int DecisionNumber { get; init; }
    public int DecisionYear { get; init; }
    public DateOnly DecisionDate { get; init; }
    public string Content { get; init; } = string.Empty;
    public string? Attachment { get; init; }
}