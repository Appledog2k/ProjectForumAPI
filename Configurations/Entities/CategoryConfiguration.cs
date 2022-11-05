using Articles.Models.Data.AggregateCategories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Articles.Configuration.Entities
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(options => options.Id);
            builder.Property(x => x.Description).HasMaxLength(500).IsRequired(true);
        }
    }
}