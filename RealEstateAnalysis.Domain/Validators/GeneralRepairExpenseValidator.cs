using FluentValidation;
using RealEstateAnalysis.Domain.DTOs.Writes;
using System;

namespace RealEstateAnalysis.Domain.Validators
{
    internal class GeneralRepairExpenseValidator : AbstractValidator<WriteGeneralRepairExpense>
    {
        public GeneralRepairExpenseValidator()
        {
            RuleFor(x => x.GeneralRepairExpenseTypeId)
                .GreaterThan(0)
                .WithMessage("General repair expense type ID is required.");

            RuleFor(x => x.Amount)
                .InclusiveBetween(0, Decimal.MaxValue)
                .WithMessage("Valid general repair expense amount is required.");
        }
    }
}