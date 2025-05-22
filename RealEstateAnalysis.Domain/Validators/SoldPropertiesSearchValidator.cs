using FluentValidation;
using RealEstateAnalysis.Domain.DTOs.Writes;

namespace RealEstateAnalysis.Domain.Validators
{
    public class SoldPropertiesSearchValidator : AbstractValidator<WriteSoldPropertiesSearch>
    {
        public SoldPropertiesSearchValidator()
        {
            RuleFor(x => x.PropertyTypeId)
                .NotEmpty()
                .WithMessage("Property Type Id is required.");

            RuleFor(x => x.ZipCode)
                .Matches(@"^\d{5}(-\d{4})?$")
                .WithMessage("Invalid zip code.");

            RuleFor(x => x.ZipCodeRadiusInMiles)
                .InclusiveBetween(0, 50)
                .WithMessage("Zip code radius must be from 0 to 50 miles.");

            RuleFor(x => x.SubjectProperty)
                .SetValidator(new AddressValidator())
                .When(x => x.SubjectProperty != null && string.IsNullOrWhiteSpace(x.SubjectProperty.Address) == false);
        }
    }
}
