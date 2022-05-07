namespace Articles.Repository
{
    public interface ISendMail
    {
        Task<string> SendMailAsync(string _from, string _to, string _subject, string _body, string _gmail, string _password);
    }
}