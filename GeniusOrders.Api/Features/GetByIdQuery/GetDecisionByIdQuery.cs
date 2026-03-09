using GeniusOrders.Api.Features.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GeniusOrders.Api.Features.GetByIdQuery;

public record GetDecisionByIdQuery(int id) : IRequest<DecisionDto>;
