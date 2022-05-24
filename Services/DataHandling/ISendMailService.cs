using Articles.Models;

namespace Articles.Services.DataHandling
{
    public interface ISendMailService
    {
        Task<string> SendGMailAsync(MailContent mailContent);
    }
}