using Mailing.Data;
using Mailing.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Mailing
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> mailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = mailSettings.Value ?? throw new ArgumentNullException(nameof(mailSettings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<bool> SendEmail(Email emailRequest)
        {
            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse(_emailSettings.Email);
            email.To.Add(MailboxAddress.Parse(emailRequest.To));
            email.Subject = emailRequest.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = emailRequest.Body;
            builder.TextBody = emailRequest.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_emailSettings.Host, _emailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.Email, _emailSettings.Password);

            try
            {
                _logger.LogInformation("Sending email via SMTP server {serverName}", _emailSettings.Host);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An error has occured when sending email via SMTP server {ServerName} : {ErrorMessage}", _emailSettings.Host, ex.Message);
                return false;
            }
            finally
            {
                smtp.Disconnect(true);
            }
            return true;
        }

    }
}
