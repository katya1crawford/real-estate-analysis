using FluentValidation;
using RealEstateAnalysis.Domain.DTOs.Writes;

namespace RealEstateAnalysis.Domain.Validators
{
    internal class RefreshTokenValidator : AbstractValidator<WriteRefreshToken>
    {
        public RefreshTokenValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage("Token is required.");

            RuleFor(x => x.RefreshToken)
                .NotEmpty()
                .WithMessage("Refresh token is required.");
        }
    }
}