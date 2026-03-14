using GeniusOrders.Api.Features.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GeniusOrders.Features.GetByIdQuery;

public record GetDecisionByIdQuery(int id) : IRequest<DecisionDto>;
