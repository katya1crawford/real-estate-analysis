using FluentValidation;
using RealEstateAnalysis.Domain.DTOs.Writes;

namespace RealEstateAnalysis.Domain.Validators
{
    internal class EmailValidator : AbstractValidator<WriteEmail>
    {
        public EmailValidator()
        {
            RuleFor(x => x.ToEmail)
                .NotEmpty()
                .WithMessage("To email address is required.")
                .EmailAddress()
                .WithMessage("Valid to email address is required.")
                .MaximumLength(100)
                .WithMessage("To email address must be 100 characters or less.");

            RuleFor(x => x.FromEmail)
                .NotEmpty()
                .WithMessage("From email address is required.")
                .EmailAddress()
                .WithMessage("Valid from email address is required.")
                .MaximumLength(100)
                .WithMessage("From email address must be 100 characters or less.");

            RuleFor(x => x.Subject)
                .NotEmpty()
                .WithMessage("Subject is required.")
                .MaximumLength(150)
                .WithMessage("Subject must be 150 characters or less."); ;

            RuleFor(x => x.Message)
                .NotEmpty()
                .WithMessage("Message is required.");
        }
    }
}