using FluentValidation;
using RealEstateAnalysis.Data.Entities.Lookups;
using RealEstateAnalysis.Domain.DTOs.Writes.LocationAnalysis;
using RealEstateAnalysis.Domain.Extensions;

namespace RealEstateAnalysis.Domain.Validators.LocationAnalysis
{
    public class WriteCityValidator : ExceptionThrowingValidator<WriteCityValidator.ValidationState>
    {
        public WriteCityValidator()
        {
            RuleFor(x => x.Model.CityName)
                .NotEmpty()
                .WithMessage("City name is required.")
                .MaximumLength(500)
                .WithMessage("City name must be less than 500 characters long.");

            RuleFor(x => x.Model.PopulationInYearStart)
                .GreaterThan(0)
                .WithMessage("Population in year start must be greater than zero.")
                .LessThan(999999999)
                .WithMessage("Invalid population number in year start.");

            RuleFor(x => x.Model.PopulationInYearEnd)
                .GreaterThan(0)
                .WithMessage("Population in year end must be greater than zero.")
                .LessThan(999999999)
                .WithMessage("Invalid population number in year end.");

            RuleFor(x => x.Model.MedianHouseholdIncomeInYearStart)
                .GreaterThan(0)
                .WithMessage("Median household income in year start must be greater than zero.");

            RuleFor(x => x.Model.MedianHouseholdIncomeInYearEnd)
                .GreaterThan(0)
                .WithMessage("Median household income in year end must be greater than zero.");

            RuleFor(x => x.Model.MedianHouseOrCondoValueInYearStart)
                .GreaterThan(0)
                .WithMessage("Median house or condo value in year start must be greater than zero.");

            RuleFor(x => x.Model.MedianHouseOrCondoValueInYearEnd)
                .GreaterThan(0)
                .WithMessage("Median house or condo value in year end must be greater than zero.");

            RuleFor(x => x.Model.StateId)
                .GreaterThan(0)
                .WithMessage("State ID is required.");

            RuleFor(x => x.Model.CrimeIndexInYearStart)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Crime index in year start must be greater than or equal to zero.");

            RuleFor(x => x.Model.CrimeIndexInYearEnd)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Crime index in year end must be greater than or equal to zero.");

            RuleFor(x => x.Model.CrimeIndexYearEnd)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Crime index year end must be greater than or equal to zero.");

            RuleFor(x => x.Model.CrimeIndexYearStart)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Crime index year start must be greater than or equal to zero.");

            RuleFor(x => x.Model.CrimeIndexYearEnd)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Crime index year end must be greater than or equal to zero.")
                .GreaterThanOrEqualTo(x => x.Model.CrimeIndexYearStart)
                .When(x => x.Model.CrimeIndexInYearEnd > 0)
                .WithMessage("Crime index year end must be after crime index year start.");

            RuleFor(x => x.Model.PopulationYearEnd)
                .GreaterThan(0)
                .WithMessage("Population year end must be greater than zero.")
                .GreaterThanOrEqualTo(2017)
                .WithMessage("Population year end must be greater or equal to 2017.");

            RuleFor(x => x.Model.PopulationYearStart)
                .GreaterThan(0)
                .WithMessage("Population year start must be greater than zero.")
                .LessThan(x => x.Model.PopulationYearEnd)
                .WithMessage("Population year start must be before population year end.");

            RuleFor(x => x.Model.MedianHouseholdIncomeYearStart)
                .GreaterThan(0)
                .WithMessage("Median household income year start must be greater than zero.")
                .LessThan(x => x.Model.MedianHouseholdIncomeYearEnd)
                .WithMessage("Median household income year start must be before median household income year end.");

            RuleFor(x => x.Model.MedianHouseOrCondoValueYearStart)
                .GreaterThan(0)
                .WithMessage("Median house or condo value year start must be greater than zero.")
                .LessThan(x => x.Model.MedianHouseOrCondoValueYearEnd)
                .WithMessage("Median house or condo value year start must be before Median house or condo value year end.");

            RuleFor(x => x.Model.MedianHouseholdIncomeYearEnd)
                .GreaterThan(0)
                .WithMessage("Median household income year end must be greater than zero.")
                .GreaterThanOrEqualTo(2016)
                .WithMessage("Median household income year end must be greater or equal to 2016.");

            RuleFor(x => x.Model.MedianHouseOrCondoValueYearEnd)
                .GreaterThan(0)
                .WithMessage("Median house or condo value year end must be greater than zero.")
                .GreaterThanOrEqualTo(2016)
                .WithMessage("Median house or condo value year end must be greater or equal to 2016.");

            RuleFor(x => x.State)
                .NotNull()
                .WithMessage("Invalid state Id.");
        }

        public class ValidationState
        {
            public WriteCity Model { get; set; }

            public State State { get; set; }
        }
    }
}