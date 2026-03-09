using AutoMapper;

namespace GeniusOrders.Api.Features.GetAllQuery;

public record GetAllDecisionDto
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
        CreateMap<Entities.Decision, GetAllDecisionDto>();
    }
}