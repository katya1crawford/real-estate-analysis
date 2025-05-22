using FluentValidation;
using FluentValidation.Results;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Extensions
{
    public class ExceptionThrowingValidator<T> : AbstractValidator<T>
    {
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            ValidationResult result;

            if (context.InstanceToValidate == null)
            {
                result = GetNullResult();
            }
            else
            {
                result = base.Validate(context);
            }

            return ThrowIfInvalid(result);
        }

        private ValidationResult ThrowIfInvalid(ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return validationResult;
        }

        public override async Task<ValidationResult> ValidateAsync(ValidationContext<T> context, CancellationToken cancellation = default)
        {
            ValidationResult result;

            if (context.InstanceToValidate == null)
            {
                result = GetNullResult();
            }
            else
            {
                result = await base.ValidateAsync(context, cancellation);
            }

            return ThrowIfInvalid(result);
        }

        private ValidationResult GetNullResult() =>
            new ValidationResult(new[] { new ValidationFailure(typeof(T).Name, $"Object to validate was null") });
    }
}
