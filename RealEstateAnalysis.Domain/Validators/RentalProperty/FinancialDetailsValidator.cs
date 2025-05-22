using FluentValidation;
using RealEstateAnalysis.Domain.DTOs.Writes.RentalProperty;

namespace RealEstateAnalysis.Domain.Validators.RentalProperty
{
    internal class FinancialDetailsValidator : AbstractValidator<WriteFinancialSummary>
    {
        public FinancialDetailsValidator()
        {
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
                .WithMessage("Annual gross scheduled rental income is required.")
                .LessThanOrEqualTo(decimal.MaxValue)
                .WithMessage("Invalid annual rent income.");

            RuleFor(x => x.OtherAnnualIncome)
                .InclusiveBetween(0, decimal.MaxValue)
                .WithMessage("Valid other annual income is required.");

            RuleFor(x => x.AnnualVacancyRate)
                .InclusiveBetween(0, 100)
                .WithMessage("Annual vacancy rate must be between 0 and 100.");

            RuleFor(x => x.AnnualGrossScheduledRentalIncomeGrowthRate)
                .InclusiveBetween(0, 100)
                .WithMessage("Annual rent income growth rate must be between 0 and 100.");

            RuleFor(x => x.AnnualOperatingExpensesGrowthRate)
                .InclusiveBetween(0, 100)
                .WithMessage("Annual operating expenses growth rate must be between 0 and 100.");

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