using Articles.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Articles.Configuration.Entities
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasData(
                new Article
                {
                    Id = 1,
                    Title = "Article 1",
                    Created = new System.DateTime(2019, 1, 1),
                    Content = "Content of article 1",
                    AuthorId = 1
                },
                new Article
                {
                    Id = 2,
                    Title = "Article 2",
                    Created = new System.DateTime(2019, 1, 1),
                    Content = "Content of article 2",
                    AuthorId = 2
                },
                new Article
                {
                    Id = 3,
                    Title = "Article 3",
                    Created = new System.DateTime(2019, 1, 1),
                    Content = "Content of article 3",
                    AuthorId = 3
                }
            );
            builder.HasKey(options => options.Id);
            builder.HasOne(options => options.Author).WithMany(options => options.Articles).HasForeignKey(options => options.AuthorId);
        }
    }
}