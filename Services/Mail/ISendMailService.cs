using Articles.Models;

namespace Articles.Services.Mail
{
    public interface ISendMailService
    {
        Task<string> SendGMailAsync(MailContent mailContent);
    }
}