using FluentValidation;
using MediatR;

namespace GeniusOrders.Behaviors;

public class ValidationBehavior<IRequest, IResponse>(IEnumerable<IValidator<IRequest>> validators)
    : IPipelineBehavior<IRequest, IResponse> where IRequest : notnull
{
    public async Task<IResponse> Handle(IRequest request, RequestHandlerDelegate<IResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return await next(cancellationToken);

        var context = new ValidationContext<IRequest>(request);
        var results = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures = results.SelectMany(r => r.Errors).Where(f => f != null).ToList();

        if (failures.Count != 0)
            throw new ValidationException(failures);

        return await next(cancellationToken);


    }
}