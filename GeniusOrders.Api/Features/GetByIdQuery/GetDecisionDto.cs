using AutoMapper;

namespace GeniusOrders.Api.Features.GetByIdQuery;

public record GetDecisionDto
(
    int Id,
    int DecisionNumber,
    int DecisionYear,
    DateOnly DecisionDate,
    string Content,
    string? Attachment
);


public class DecisionProfile : Profile
{
    public DecisionProfile()
    {
        CreateMap<Entities.Decision, GetDecisionDto>();
    }
}