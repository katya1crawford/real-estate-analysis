using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace RealEstateAnalysis.Domain.Validators
{
    internal class ThumbnailImageValidator : AbstractValidator<IFormFile>
    {
        private List<string> acceptableFileTypes = new List<string> { ".jpg", ".jpeg", ".png" };

        public ThumbnailImageValidator()
        {
            RuleFor(x => x.FileName)
                .Must(x => acceptableFileTypes.Contains(Path.GetExtension(x.ToLower())))
                .WithMessage($"Only {string.Join(", ", acceptableFileTypes)} files are acceptable.");

            RuleFor(x => x.Length)
                .LessThanOrEqualTo(5242880)
                .WithMessage("File size must be equal or less than 5 MB.");
        }
    }
}