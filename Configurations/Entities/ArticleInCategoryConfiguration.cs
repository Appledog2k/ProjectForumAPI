using Articles.Models.Data.AggregateArticleInCategory;
using Articles.Models.Data.AggregateCategories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Articles.Configuration.Entities
{
    public class ArticleInCategoryConfiguration : IEntityTypeConfiguration<ArticleInCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleInCategory> builder)
        {
            builder.HasKey(options => options.Id);
            builder.HasOne(x => x.Article).WithMany(x => x.ArticleInCategories).HasForeignKey(x => x.ArticleId);
            builder.HasOne(x => x.Category).WithMany(x => x.ArticleInCategories).HasForeignKey(x => x.CategoryId);
        }
    }
}