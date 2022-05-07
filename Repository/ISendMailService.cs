using Articles.Models;

namespace Articles.Repository
{
    public interface ISendMailService
    {
        Task<string> SendGMailAsync(MailContent mailContent);
    }
}