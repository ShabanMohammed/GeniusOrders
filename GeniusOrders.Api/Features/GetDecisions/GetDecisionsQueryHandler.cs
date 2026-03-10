using GeniusOrders.Api.Data;
using GeniusOrders.Api.Features.Commond;
using GeniusOrders.Api.Features.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GeniusOrders.Api.Features.GetDecisions;

public class GetDecisionsQueryHandler(GeniusDbContext context) : IRequestHandler<GetDecisionsQuery, PagedResponse<DecisionDto>>
{

    public async Task<PagedResponse<DecisionDto>> Handle(GetDecisionsQuery request, CancellationToken cancellationToken)
    {
        var query = context.Decisions.AsNoTracking().AsQueryable();

        if (request.DecisionNumber.HasValue)
            query = query.Where(d => d.DecisionNumber == request.DecisionNumber.Value);

        if (request.Year.HasValue)
            query = query.Where(d => d.DecisionYear == request.Year.Value);

        if (!string.IsNullOrEmpty(request.SearchTerm))
            query = query.Where(d => d.Content.Contains(request.SearchTerm));

        if (request.DateFrom.HasValue)
            query = query.Where(d => d.DecisionDate >= request.DateFrom.Value);

        if (request.DateTo.HasValue)
            query = query.Where(d => d.DecisionDate <= request.DateTo.Value);

        var totalItems = await query.CountAsync(cancellationToken);
        var decisions = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(d => new DecisionDto
            {
                Id = d.Id,
                DecisionNumber = d.DecisionNumber,
                DecisionYear = d.DecisionYear,
                DecisionDate = d.DecisionDate,
                Content = d.Content
            }).OrderByDescending(d => d.DecisionYear).ThenByDescending(d => d.DecisionNumber)
            .ToListAsync(cancellationToken);

        return new PagedResponse<DecisionDto>(decisions, totalItems, request.PageNumber, request.PageSize);
    }
}