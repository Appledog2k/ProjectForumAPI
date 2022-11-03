using Articles.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Articles.Configuration.Entities
{
    public class ArticleImageConfiguration : IEntityTypeConfiguration<ImageArticle>
    {
        public void Configure(EntityTypeBuilder<ImageArticle> builder)
        {
            builder.ToTable("ImageArticles");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.ImagePath).HasMaxLength(200).IsRequired(true);
            builder.Property(x => x.Caption).HasMaxLength(200).IsRequired(false);
            builder.HasOne(x => x.Article).WithMany(x => x.ImageArticles).HasForeignKey(x => x.ArticleId);
        }
    }
}