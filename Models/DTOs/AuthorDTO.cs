namespace Articles.Models.DTOs
{
    public class Create_AuthorDTO
    {
        public string Name { get; set; }
    }
    public class AuthorDTO : Create_AuthorDTO
    {
        public int Id { get; set; }
        public IList<ArticleDTO> Articles { get; set; }
    }
}