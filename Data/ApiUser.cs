using Microsoft.AspNetCore.Identity;
namespace Articles.Data
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public List<Article>? Articles { get; set; }
    }
}