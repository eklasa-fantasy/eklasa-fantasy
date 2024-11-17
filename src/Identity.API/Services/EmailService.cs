using Identity.API.Interfaces;
using System.Text.RegularExpressions;
using Identity.API.Options;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Identity.API.Services
{
    public class EmailService : IEmailService
    {
    private readonly IConfiguration _configuration;
    private readonly IOptions<EmailSettings>  _options;
    public EmailService(IConfiguration configuration, IOptions<EmailSettings> options)
    {
         _configuration = configuration;
         _options = options;
    }   

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            
            if (string.IsNullOrWhiteSpace(htmlMessage))
                throw new ArgumentNullException(nameof(htmlMessage), "Email message content cannot be null or empty.");

            string? fromEmail = _options.Value.SenderEmail;
            string? fromName = _options.Value.SenderName;
            string? apiKey = Environment.GetEnvironmentVariable("SG_API_KEY");
            var sendGridClient = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, fromName);
            var to = new EmailAddress(email);
            var plainTextContent = Regex.Replace(htmlMessage, "<[^>]*>", "");
            var msg = MailHelper.CreateSingleEmail(from, to, subject,
            plainTextContent, htmlMessage);
            var response = await sendGridClient.SendEmailAsync(msg);
        }
    }
}