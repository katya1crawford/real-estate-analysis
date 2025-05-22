using FluentValidation;
using RealEstateAnalysis.Domain.DTOs.Writes;

namespace RealEstateAnalysis.Domain.Validators
{
    public class ConfirmEmailValidator : AbstractValidator<WriteConfirmEmail>
    {
        public ConfirmEmailValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User id is required.");

            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage("Token is required.");
        }
    }
}