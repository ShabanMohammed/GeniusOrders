using AutoMapper;

namespace GeniusOrders.Features.Dtos;

public record DecisionDto
{
    public int? Id { get; init; }
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
        CreateMap<DecisionDto, Entities.Decision>();
        CreateMap<Entities.Decision, DecisionDto>();
    }
}
