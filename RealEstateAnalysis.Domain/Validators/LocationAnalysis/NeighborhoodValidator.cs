using FluentValidation;
using RealEstateAnalysis.Domain.DTOs.Writes.LocationAnalysis;

namespace RealEstateAnalysis.Domain.Validators.LocationAnalysis
{
    public class NeighborhoodValidator : AbstractValidator<WriteNeighborhood>
    {
        public NeighborhoodValidator()
        {
            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("City name is required.")
                .MaximumLength(500)
                .WithMessage("City name must be less than 500 characters long.");

            RuleFor(x => x.NeighborhoodName)
                .NotEmpty()
                .WithMessage("Neighborhood name is required.")
                .MaximumLength(250)
                .WithMessage("Neighborhood name must be less than 250 characters long.");

            RuleFor(x => x.MedianContractRent)
                .GreaterThan(0)
                .WithMessage("Median contract rent must be greater than zero.");

            RuleFor(x => x.CityUnemploymentRate)
                .InclusiveBetween(0.01M, 100)
                .WithMessage("City unemployment rate must be inclusive between 0.01 and 100.");

            RuleFor(x => x.EthnicMixLargestSlicePercent)
                .InclusiveBetween(0.01M, 100)
                .WithMessage("Ethnic mix largest slice percent must be inclusive between 0.01 and 100.");

            RuleFor(x => x.MedianHouseholdIncome)
                .GreaterThan(0)
                .WithMessage("Median household income must be greater than zero.");

            RuleFor(x => x.PovertyRate)
                .InclusiveBetween(0.01M, 100)
                .WithMessage("Neighborhood poverty rate must be inclusive between 0.01 and 100.");

            RuleFor(x => x.NeighborhoodUnemploymentRate)
                .InclusiveBetween(0.01M, 100)
                .WithMessage("Neighborhood unemployment rate must be inclusive between 0.01 and 100.");

            RuleFor(x => x.HomesMedianDaysOnMarket)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Homes median days on market must be greater than or equal to zero.");

            RuleFor(x => x.StateId)
                .GreaterThan(0)
                .WithMessage("State ID is required.");
        }
    }
}