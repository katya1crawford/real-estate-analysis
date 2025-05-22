using FluentValidation;
using RealEstateAnalysis.Domain.DTOs.Writes;

namespace RealEstateAnalysis.Domain.Validators
{
    internal class AddressValidator : AbstractValidator<WriteAddress>
    {
        public AddressValidator()
        {
            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Address is required.")
                .MinimumLength(5)
                .WithMessage("Address must be at least 5 characters long.")
                .MaximumLength(500)
                .WithMessage("Address must be less than 500 characters long.");

            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("City is required.")
                .MaximumLength(500)
                .WithMessage("City must be less than 500 characters long.");

            RuleFor(x => x.StateId)
                .NotEqual(0)
                .WithMessage("State is required.");

            RuleFor(x => x.ZipCode)
                .NotEmpty()
                .WithMessage("Zip Code is required.")
                .Matches(@"^\d{5}(-\d{4})?$")
                .WithMessage("Invalid zip code.");
        }
    }
}