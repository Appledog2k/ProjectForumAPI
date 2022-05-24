namespace Articles.Models.DTOs
{
    public class LoginUserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
    public class UserDTO : LoginUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ConfirmPassword { get; set; }
        public ICollection<string> Roles { get; set; }
    }

}