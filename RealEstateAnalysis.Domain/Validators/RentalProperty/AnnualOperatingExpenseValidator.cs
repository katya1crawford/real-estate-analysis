using FluentValidation;
using RealEstateAnalysis.Domain.DTOs.Writes.RentalProperty;
using System;

namespace RealEstateAnalysis.Domain.Validators.RentalProperty
{
    internal class AnnualOperatingExpenseValidator : AbstractValidator<WriteAnnualOperatingExpense>
    {
        public AnnualOperatingExpenseValidator()
        {
            RuleFor(x => x.OperatingExpenseTypeId)
                .GreaterThan(0)
                .WithMessage("Annual operating expense type ID is required.");

            RuleFor(x => x.Amount)
                .InclusiveBetween(0, Decimal.MaxValue)
                .WithMessage("Valid operating expense amount is required.");
        }
    }
}