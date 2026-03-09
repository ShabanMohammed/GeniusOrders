using MediatR;

namespace GeniusOrders.Api.Features.GetAllQuery;

public record GetAllDecisionQuery : IRequest<IEnumerable<GetAllDecisionDto>>;
