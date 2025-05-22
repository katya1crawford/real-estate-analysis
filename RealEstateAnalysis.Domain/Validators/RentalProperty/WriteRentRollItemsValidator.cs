using FluentValidation;
using RealEstateAnalysis.Data.Entities.RentalProperty;
using RealEstateAnalysis.Domain.DTOs.Writes.RentalProperty;
using RealEstateAnalysis.Domain.Extensions;
using System.Collections.Generic;

namespace RealEstateAnalysis.Domain.Validators.RentalProperty
{
    public class WriteRentRollItemsValidator : ExceptionThrowingValidator<WriteRentRollItemsValidator.ValidationState>
    {
        public WriteRentRollItemsValidator()
        {
            RuleFor(x => x.Property)
                .NotNull()
                .WithMessage("Invalid property id.");

            RuleFor(x => x.Models)
                .NotEmpty()
                .WithMessage("Rent roll items are required.")
                .DependentRules(() =>
                {
                    RuleForEach(x => x.Models)
                        .ChildRules(rentRollItem =>
                        {
                            rentRollItem.RuleFor(x => x.Unit)
                                .NotEmpty()
                                .WithMessage("Unit is required")
                                .MaximumLength(250)
                                .WithMessage("Unit must be no more than 250 characters.");

                            rentRollItem.RuleFor(x => x.ContractRent)
                                .GreaterThan(0)
                                .When(x => x.IsVacant == false)
                                .WithMessage(x => $"Unit: {x.Unit}. Contract rent must be greater than 0 when unit is occupied.");

                            rentRollItem.RuleFor(x => x.Bedrooms)
                                .GreaterThan(0)
                                .WithMessage(x => $"Unit: {x.Unit}. Number of bedrooms must be greater than 0.");

                            rentRollItem.RuleFor(x => x.Bathrooms)
                                .GreaterThan(0)
                                .WithMessage(x => $"Unit: {x.Unit}. Number of bathrooms must be greater than 0.");

                            rentRollItem.RuleFor(x => x.SquareFootage)
                                .GreaterThan(0)
                                .WithMessage(x => $"Unit: {x.Unit}. Square footage must be greater than 0.");

                            rentRollItem.RuleFor(x => x.LeaseStartDate)
                                .NotNull()
                                .When(x => x.LeaseEndDate.HasValue)
                                .WithMessage(x => $"Unit: {x.Unit}. Lease start date is missing.");

                            rentRollItem.RuleFor(x => x.LeaseEndDate)
                                .NotNull()
                                .When(x => x.LeaseStartDate.HasValue)
                                .WithMessage(x => $"Unit: {x.Unit}. Lease end date is missing.");

                            rentRollItem.RuleFor(x => x.LeaseEndDate)
                                .GreaterThan(x => x.LeaseStartDate)
                                .When(x => x.LeaseStartDate.HasValue && x.LeaseEndDate.HasValue)
                                .WithMessage(x => $"Unit: {x.Unit}. Lease end date must be after lease start date");

                            rentRollItem.When(x => x.IsVacant, () =>
                            {
                                rentRollItem.RuleFor(x => x.LeaseStartDate)
                                    .Null()
                                    .WithMessage(x => $"Unit: {x.Unit}. Lease start date must be empty when unit is vacant.");

                                rentRollItem.RuleFor(x => x.LeaseEndDate)
                                    .Null()
                                    .WithMessage(x => $"Unit: {x.Unit}. Lease end date must be empty when unit is vacant.");

                                rentRollItem.RuleFor(x => x.ContractRent)
                                    .Null()
                                    .WithMessage(x => $"Unit: {x.Unit}. Contract rent must be empty when unit is vacant.");
                            });
                        });
                });
        }

        public class ValidationState
        {
            public ValidationState(Property property, List<WriteRentRollItem> models)
            {
                Property = property;
                Models = models;
            }

            public Property Property { get; }

            public List<WriteRentRollItem> Models { get; set; }
        }
    }
}