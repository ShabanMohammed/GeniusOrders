using AutoMapper;

namespace GeniusOrders.Api.Features.CreateDecision;

public record CreateDecisionDto
{
    public int DecisionNumber { get; init; }
    public int DecisionYear { get; init; }
    public DateOnly DecisionDate { get; init; }
    public string Content { get; init; } = string.Empty;
    public string? Attachment { get; init; }
}

public class DecisionProfile : Profile
{
    public DecisionProfile()
    {
        CreateMap<CreateDecisionDto, Entities.Decision>();
    }
}
