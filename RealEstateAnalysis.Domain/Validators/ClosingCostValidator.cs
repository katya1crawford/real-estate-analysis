using FluentValidation;
using RealEstateAnalysis.Domain.DTOs.Writes;
using System;

namespace RealEstateAnalysis.Domain.Validators
{
    public class ClosingCostValidator : AbstractValidator<WriteClosingCost>
    {
        public ClosingCostValidator()
        {
            RuleFor(x => x.ClosingCostTypeId)
                .GreaterThan(0)
                .WithMessage("Closing cost type ID is required.");

            RuleFor(x => x.Amount)
                .InclusiveBetween(0, Decimal.MaxValue)
                .WithMessage("Valid general repair expense amount is required.");
        }
    }
}