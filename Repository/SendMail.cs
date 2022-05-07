using System.Net;
using System.Net.Mail;

namespace Articles.Repository
{
    public class SendMail : ISendMail
    {
        private readonly IConfiguration _configuration;
        public SendMail(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> SendMailAsync(string _from, string _to, string _subject, string _body, string _gmail, string _password)
        {
            // Tạo nội dung Email
            MailMessage message = new MailMessage(_from, _to, _subject, _body);
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;

            message.ReplyToList.Add(new MailAddress(_from));
            message.Sender = new MailAddress(_from);

            using var smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_gmail, _password);

            try
            {
                await smtpClient.SendMailAsync(message);
                return "Gui mail thanh cong";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Gui mail that bai: " + ex.Message;
            }

        }

    }
}