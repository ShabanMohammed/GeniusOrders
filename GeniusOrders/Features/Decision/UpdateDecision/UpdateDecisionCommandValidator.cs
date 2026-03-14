using FluentValidation;
using GeniusOrders.Data;
using GeniusOrders.Entities;
using Microsoft.EntityFrameworkCore;


namespace GeniusOrders.Features.UpdateDecision;

public class UpdateDecisionCommandValidator : AbstractValidator<UpdateDecisionCommand>
{
    public UpdateDecisionCommandValidator(GeniusDbContext dbContext)
    {

        RuleFor(x => x.DecisionNumber).GreaterThan(0).WithMessage("رقم القرار يجب أن يكون أكبر من صفر.");
        RuleFor(x => x.DecisionYear).InclusiveBetween(1900, 2999).WithMessage("سنة القرار يجب أن تكون بين 1900 و 2999.");
        RuleFor(x => x).Must(x => x.DecisionDate.Year == x.DecisionYear)
            .WithMessage("سنة تاريخ القرار يجب أن تتطابق مع سنة القرار.");
        RuleFor(x => x).MustAsync(async (x, c) =>
        {
            return !await dbContext.Decisions.AnyAsync((Decision d) => d.DecisionNumber == x.DecisionNumber
                                                                         && d.DecisionYear == x.DecisionYear && d.Id != x.Id);
        }
        ).WithMessage("رقم القرار وسنة القرار يجب أن يكونا فريدين.");
    }
}