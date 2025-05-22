using FluentValidation;
using RealEstateAnalysis.Data.Entities.LocationAnalysis;
using RealEstateAnalysis.Domain.Extensions;

namespace RealEstateAnalysis.Domain.Validators.LocationAnalysis
{
    public class ToggleIsFavoriteValidator : ExceptionThrowingValidator<ToggleIsFavoriteValidator.ValidationState>
    {
        public ToggleIsFavoriteValidator()
        {
            RuleFor(x => x.City)
                .NotNull()
                .WithMessage("Invalid city id.");
        }

        public class ValidationState
        {
            public City City { get; set; }
        }
    }
}