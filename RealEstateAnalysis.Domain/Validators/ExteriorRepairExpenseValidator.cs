using FluentValidation;
using RealEstateAnalysis.Domain.DTOs.Writes;
using System;

namespace RealEstateAnalysis.Domain.Validators
{
    internal class ExteriorRepairExpenseValidator : AbstractValidator<WriteExteriorRepairExpense>
    {
        public ExteriorRepairExpenseValidator()
        {
            RuleFor(x => x.ExteriorRepairExpenseTypeId)
                .GreaterThan(0)
                .WithMessage("Exterior repair expense type ID is required.");

            RuleFor(x => x.Amount)
                .InclusiveBetween(0, Decimal.MaxValue)
                .WithMessage("Valid exterior repair expense is required.");
        }
    }
}