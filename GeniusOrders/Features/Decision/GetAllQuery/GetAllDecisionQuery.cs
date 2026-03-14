using GeniusOrders.Client.Dtos;
using MediatR;

namespace GeniusOrders.Features.GetAllQuery;

public record GetAllDecisionQuery : IRequest<IEnumerable<DecisionDto>>;
