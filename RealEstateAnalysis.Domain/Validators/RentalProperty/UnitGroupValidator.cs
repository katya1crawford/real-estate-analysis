using FluentValidation;
using RealEstateAnalysis.Domain.DTOs.Writes;

namespace RealEstateAnalysis.Domain.Validators.RentalProperty
{
    public class UnitGroupValidator : AbstractValidator<WriteUnitGroup>
    {
        public UnitGroupValidator()
        {
            RuleFor(x => x.Bathrooms)
                .GreaterThan(0)
                .WithMessage("Number of bathrooms is required.")
                .LessThanOrEqualTo(double.MaxValue)
                .WithMessage("Invalid number of bathrooms.");

            RuleFor(x => x.Bedrooms)
                .GreaterThan(0)
                .WithMessage("Number of bedrooms is required.")
                .LessThanOrEqualTo(int.MaxValue)
                .WithMessage("Invalid number of bedrooms.");

            RuleFor(x => x.NumberOfUnits)
                .GreaterThan(0)
                .WithMessage("Number of units is required.")
                .LessThanOrEqualTo(int.MaxValue)
                .WithMessage("Invalid number of units.");

            RuleFor(x => x.SquareFootage)
                .GreaterThan(0)
                .WithMessage("Unit(s) square footage is required.")
                .LessThanOrEqualTo(int.MaxValue)
                .WithMessage("Invalid unit(s) square footage.");
        }
    }
}