using Articles.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Articles.Data
{
    public class ArticleContext : IdentityDbContext<AppUser>
    {
        public ArticleContext(DbContextOptions<ArticleContext> options) : base(options)
        {
            //..
        }
        public DbSet<Article> Articles { get; set; }
    }
}

