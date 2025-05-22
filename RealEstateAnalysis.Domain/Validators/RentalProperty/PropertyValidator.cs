using FluentValidation;
using RealEstateAnalysis.Domain.DTOs.Writes.RentalProperty;
using System;

namespace RealEstateAnalysis.Domain.Validators.RentalProperty
{
    internal class PropertyValidator : AbstractValidator<WriteProperty>
    {
        public PropertyValidator()
        {
            RuleFor(x => x.ThumbnailImage)
                .SetValidator(new ThumbnailImageValidator());

            RuleFor(x => x.PropertyTypeId)
                .GreaterThan(0)
                .WithMessage("Property type is required.");

            RuleFor(x => x.LotSquareFootage)
                .InclusiveBetween(0, int.MaxValue)
                .WithMessage("Lot square footage must be greater or equal to zero.");

            RuleFor(x => x.YearBuiltIn)
                .InclusiveBetween(1800, DateTime.Now.Year)
                .WithMessage("Valid year built in is required.");

            RuleFor(x => x.BuildingSquareFootage)
                .GreaterThan(0)
                .WithMessage("Building square footage is required.")
                .LessThanOrEqualTo(int.MaxValue)
                .WithMessage("Valid building square footage is required.");

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Address object is required.")
                .SetValidator(new AddressValidator());

            RuleFor(x => x.PurchasePrice)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Purchase price is required.")
                .LessThanOrEqualTo(decimal.MaxValue)
                .WithMessage("Invalid purchase price.");

            RuleFor(x => x.DownPayment)
                .InclusiveBetween(0, decimal.MaxValue)
                .WithMessage("Valid down payment is required.")
                .Must((model, field) => field <= model.PurchasePrice)
                .WithMessage("Down payment must be less or equal to purchase price.");

            RuleFor(x => x.LoanApr)
                .InclusiveBetween(0, 100)
                .WithMessage("Loan APR must be between 0 and 100.");

            RuleFor(x => x.LoanYears)
                .InclusiveBetween(1, 50)
                .When(x => (x.PurchasePrice - x.DownPayment) > 0)
                .WithMessage("Loan period (years) must be between 1 and 50.");

            RuleFor(x => x.AnnualGrossScheduledRentalIncome)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Annual rent income is required.")
                .LessThanOrEqualTo(decimal.MaxValue)
                .WithMessage("Invalid annual rent income.");

            RuleFor(x => x.OtherAnnualIncome)
                .InclusiveBetween(0, decimal.MaxValue)
                .WithMessage("Valid other annual income is required.");

            RuleFor(x => x.AnnualVacancyRate)
                .InclusiveBetween(0, 100)
                .WithMessage("Annual vacancy rate must be between 0 and 100.");

            RuleFor(x => x.AnnualPropertyManagementFeeRate)
                .InclusiveBetween(0, 100)
                .WithMessage("Annual property management fee rate must be between 0 and 100.");

            RuleFor(x => x.AnnualGrossScheduledRentalIncomeGrowthRate)
                .InclusiveBetween(0, 100)
                .WithMessage("Annual gross scheduled rental income growth rate must be between 0 and 100.");

            RuleFor(x => x.AnnualOperatingExpensesGrowthRate)
                .InclusiveBetween(0, 100)
                .WithMessage("Annual operating expenses growth rate must be between 0 and 100.");

            RuleFor(x => x.MarketCapitalizationRate)
                .InclusiveBetween(0, 100)
                .WithMessage("Market capitalization rate must be between 0 and 100.");

            RuleFor(x => x.GroupName)
                .MaximumLength(50)
                .WithMessage("Group name must be 50 characters or less.");

            RuleForEach(x => x.UnitGroups)
                .NotEmpty()
                .SetValidator(new UnitGroupValidator());

            RuleForEach(x => x.AnnualOperatingExpenses)
                .SetValidator(new AnnualOperatingExpenseValidator());

            RuleForEach(x => x.ClosingCosts)
                .SetValidator(new ClosingCostValidator());

            RuleForEach(x => x.InteriorRepairExpenses)
                .SetValidator(new InteriorRepairExpenseValidator());

            RuleForEach(x => x.ExteriorRepairExpenses)
                .SetValidator(new ExteriorRepairExpenseValidator());

            RuleForEach(x => x.GeneralRepairExpenses)
                .SetValidator(new GeneralRepairExpenseValidator());
        }
    }
}