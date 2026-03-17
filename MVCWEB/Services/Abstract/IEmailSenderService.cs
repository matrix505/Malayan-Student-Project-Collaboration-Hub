using MVCWEB.Models;

namespace MVCWEB.Services.Abstract
{
    public interface IEmailSenderService
    {
        Task<MailResult> SendEmailAsync(string email,string subject, string body);
    }
}
