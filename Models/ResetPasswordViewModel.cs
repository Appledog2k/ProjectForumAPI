using System.ComponentModel.DataAnnotations;

namespace Articles.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Token { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]

        public string NewPassword { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public String ConfirmPassword { get; set; }
    }
}