using FluentValidation;
using RealEstateAnalysis.Data.Entities.RentalProperty;
using RealEstateAnalysis.Domain.DTOs.Writes.RentalProperty;
using RealEstateAnalysis.Domain.Extensions;
using System.IO;

namespace RealEstateAnalysis.Domain.Validators.RentalProperty
{
    public class ImportRentRollCsvValidator : ExceptionThrowingValidator<ImportRentRollCsvValidator.ValidationState>
    {
        public ImportRentRollCsvValidator()
        {
            RuleFor(x => x.Property)
                .NotNull()
                .WithMessage("Invalid property id.");

            RuleFor(x => x.Model.RentRollCsv)
                .NotNull()
                .WithMessage("CSV file is required")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Model.RentRollCsv.Length)
                        .GreaterThan(0)
                        .WithMessage("Rent roll CSV file is required.")
                        .LessThanOrEqualTo(15728640)
                        .WithMessage("File size must be equal or less than 15 MB.");

                    RuleFor(x => x.Model.RentRollCsv.FileName)
                        .Must(x => Path.GetExtension(x.ToLower()) == ".csv")
                        .WithMessage("Only .csv file is acceptable.");
                });
        }


        public class ValidationState
        {
            public ValidationState(Property property, WriteImportRentRollCsv model)
            {
                Property = property;
                Model = model;
            }

            public Property Property { get; }

            public WriteImportRentRollCsv Model { get; }
        }
    }
}
