using Articles.Models.Data.AggregateArticles;
using Microsoft.AspNetCore.Identity;
namespace Articles.Models.Data.AggregateUsers
{
    public class ApiUser : IdentityUser
    {
        public ApiUser()
        {

        }
        public ApiUser(string email)
        {
            Email = email;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }

        /// <summary>
        /// Bài viết của đối tượng
        /// </summary>
        public ICollection<Article> Articles { get; set; }
    }
}