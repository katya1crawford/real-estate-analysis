using FluentValidation;
using Microsoft.Extensions.Options;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.DTOs.Writes;
using RealEstateAnalysis.Domain.Settings;
using RealEstateAnalysis.Domain.Validators;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Services
{
    public class ContactUsService : IContactUsService
    {
        private readonly IEmailService emailService;
        private readonly AppSettings appSettings;

        public ContactUsService(IEmailService emailService, IOptions<AppSettings> appOptions)
        {
            this.emailService = emailService;
            appSettings = appOptions.Value;
        }

        public async Task SendEmailAsync(WriteContactUs model)
        {
            var validator = new ContactUsValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var emailModel = new WriteEmail
            {
                ToEmail = appSettings.SupportEmail,
                FromEmail = appSettings.SupportEmail,
                Subject = model.Subject,
                Message = $"From email: {model.FromEmail}<br />{model.Message}"
            };

            await emailService.SendAsync(emailModel);
        }
    }
}