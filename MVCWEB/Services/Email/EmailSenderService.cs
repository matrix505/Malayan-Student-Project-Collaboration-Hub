using Microsoft.Extensions.Options;
using MimeKit;
using MVCWEB.Config;
using MVCWEB.Services.Abstract;
using MailKit.Net.Smtp;
using MailKit.Security;
using MVCWEB.Models;

namespace MVCWEB.Services.Email
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly MailSettings _settings;
        public EmailSenderService(IOptions<MailSettings> settings)
        {
            _settings = settings.Value;
        }
        public async Task<MailResult> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_settings.FromName, _settings.FromEmail));
                message.To.Add(new MailboxAddress("", toEmail));
                message.Subject = subject;
                message.Body = new TextPart("html")
                { 
                    Text = body
                };

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(
                    _settings.Host,
                    _settings.Port,
                    SecureSocketOptions.StartTls
                    );

                await smtp.AuthenticateAsync(_settings.Username,_settings.Password);
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);

                return MailResult.Ok();
            }
            catch (Exception e)
            {
                return MailResult.Fail("Mail error: " + e.Message);
            }

        }
    }
}
