using FluentValidation;
using RealEstateAnalysis.Domain.DTOs.Writes;

namespace RealEstateAnalysis.Domain.Validators
{
    internal class LoanValidator : AbstractValidator<WriteLoan>
    {
        public LoanValidator()
        {
            RuleFor(x => x.Amount)
                .InclusiveBetween(0, decimal.MaxValue)
                .WithMessage("Valid loan amount is required");

            RuleFor(x => x.Apr)
                .InclusiveBetween(0, 100)
                .WithMessage("Loan APR must be between 0 and 100.");

            RuleFor(x => x.Years)
                .InclusiveBetween(1, 50)
                .When(x => x.Amount > 0)
                .WithMessage("Loan period (years) must be between 1 and 50.");
        }
    }
}