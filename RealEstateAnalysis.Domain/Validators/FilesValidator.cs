using FluentValidation;
using RealEstateAnalysis.Domain.DTOs.Writes;

namespace RealEstateAnalysis.Domain.Validators
{
    internal class FilesValidator : AbstractValidator<WriteFiles>
    {
        public FilesValidator()
        {
            RuleFor(x => x.PropertyId)
                .GreaterThan(0)
                .WithMessage("Property ID is required.");

            RuleForEach(x => x.Files)
                .NotEmpty()
                .WithMessage("At least one file is required.")
                .SetValidator(new FileValidator());
        }
    }
}