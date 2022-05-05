using System.ComponentModel.DataAnnotations;

namespace Articles.Models
{
    public class SignInModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}