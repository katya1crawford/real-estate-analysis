using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace RealEstateAnalysis.Domain.Validators
{
    internal class ImageFileValidator : AbstractValidator<IFormFile>
    {
        private List<string> acceptableFileTypes = new List<string> { ".jpg", ".jpeg", ".png" };

        public ImageFileValidator()
        {
            RuleFor(x => x.FileName)
                .Must(x => acceptableFileTypes.Contains(Path.GetExtension(x.ToLower())))
                .WithMessage($"Only {string.Join(", ", acceptableFileTypes)} files are acceptable.");

            RuleFor(x => x.Length)
                .GreaterThan(0)
                .WithMessage("File content must not be empty.")
                .LessThanOrEqualTo(15728640)
                .WithMessage("File size must be equal or less than 15 MB.");
        }
    }
}