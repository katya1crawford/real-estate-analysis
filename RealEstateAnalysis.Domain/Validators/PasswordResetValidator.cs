using FluentValidation;
using RealEstateAnalysis.Domain.DTOs.Writes;

namespace RealEstateAnalysis.Domain.Validators
{
    internal class PasswordResetValidator : AbstractValidator<WritePasswordReset>
    {
        public PasswordResetValidator()
        {
            RuleFor(x => x.UserId).NotEmpty()
                .NotEmpty()
                .WithMessage("User ID is required.");

            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage("Token is required.");

            RuleFor(x => x.NewPassword).NotEmpty()
                .WithMessage("New password is required.")
                .MinimumLength(5)
                .WithMessage("New password must be at least 5 characters long.");
        }
    }
}