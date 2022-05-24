using Articles.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Articles.Configuration.Entities
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasData(
                new Author
                {
                    Id = 1,
                    Name = "Author 1"
                },
                new Author
                {
                    Id = 2,
                    Name = "Author 2"
                },
                new Author
                {
                    Id = 3,
                    Name = "Author 3"
                }
            );
            builder.HasKey(options => options.Id);
        }
    }
}