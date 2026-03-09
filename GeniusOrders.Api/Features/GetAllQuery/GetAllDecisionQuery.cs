using GeniusOrders.Api.Features.Dtos;
using MediatR;

namespace GeniusOrders.Api.Features.GetAllQuery;

public record GetAllDecisionQuery : IRequest<IEnumerable<DecisionDto>>;
