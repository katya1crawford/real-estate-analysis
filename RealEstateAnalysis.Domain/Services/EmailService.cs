using FluentValidation;
using Microsoft.Extensions.Options;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.DTOs.Writes;
using RealEstateAnalysis.Domain.Settings;
using RealEstateAnalysis.Domain.Validators;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Services
{
    public class EmailService : IEmailService
    {
        private readonly AppSettings appSettings;
        private readonly EmailSettings emailSettings;

        public EmailService(IOptions<EmailSettings> emailOptions, IOptions<AppSettings> appOptions)
        {
            emailSettings = emailOptions.Value;
            appSettings = appOptions.Value;
        }

        public async Task SendAsync(WriteEmail model)
        {
            var validator = new EmailValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.Host;
                smtpClient.Port = emailSettings.Port;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(model.FromEmail, appSettings.BusinessName);
                mailMessage.To.Add(new MailAddress(model.ToEmail));
                mailMessage.Subject = model.Subject;
                mailMessage.Body = model.Message;
                mailMessage.IsBodyHtml = true;

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }

                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}