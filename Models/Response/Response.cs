namespace Articles.Models.Response
{
    public class Response
    {
        public string statusCode { get; set; }
        public string message { get; set; }
        public object? developerMessage { get; set; }
        public object? data { get; set; }

        public Response(string message, object developerMessage = null, object data = null)
        {
            this.statusCode = "200";
            this.message = message;
            this.developerMessage = developerMessage;
            this.data = data;
        }
    }
}