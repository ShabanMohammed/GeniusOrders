using GeniusOrders.Api.Features.Common;
using GeniusOrders.Api.Features.Dtos;
using MediatR;

namespace GeniusOrders.Api.Features.GetDecisions;

public record GetDecisionsQuery(
    int? DecisionNumber = null,
    int? Year = null,
    string? SearchTerm = null,
    DateOnly? DateFrom = null,
    DateOnly? DateTo = null,
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<PagedResponse<DecisionDto>>;
