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
                    Title = "Đây là bài viết 1",
                    Created = new System.DateTime(2019, 1, 1),
                    Content = "Nội dung bài viết 1",
                    ViewCount = 100,
                },

                new Article
                {
                    Id = 2,
                    Title = "Đây là bài viết 2",
                    Created = new System.DateTime(2019, 1, 1),
                    Content = "Content of article 2",
                    ViewCount = 100,

                },

                new Article
                {
                    Id = 3,
                    Title = "Article 3",
                    Created = new System.DateTime(2019, 1, 1),
                    Content = "Content of article 3",
                    ViewCount = 100,
                }
            );
            // todo : Key
            builder.HasKey(options => options.Id);
            builder.HasOne(x => x.ApiUser).WithMany(x => x.Articles).HasForeignKey(x => x.UserId);
        }
    }
}
