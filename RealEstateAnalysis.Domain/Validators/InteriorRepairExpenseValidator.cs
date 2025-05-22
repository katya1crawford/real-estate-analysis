using FluentValidation;
using RealEstateAnalysis.Domain.DTOs.Writes;
using System;

namespace RealEstateAnalysis.Domain.Validators
{
    internal class InteriorRepairExpenseValidator : AbstractValidator<WriteInteriorRepairExpense>
    {
        public InteriorRepairExpenseValidator()
        {
            RuleFor(x => x.InteriorRepairExpenseTypeId)
               .GreaterThan(0)
               .WithMessage("Interior repair expense type ID is required.");

            RuleFor(x => x.Amount)
                .InclusiveBetween(0, Decimal.MaxValue)
                .WithMessage("Valid interior repair expense amount is required.");
        }
    }
}