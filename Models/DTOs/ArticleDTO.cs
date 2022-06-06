namespace Articles.Models.DTOs
{
    public class Create_ArticleDTO
    {
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
    }

    public class Update_ArticleDTO : Create_ArticleDTO
    {
    }

    public class ArticleDTO : Create_ArticleDTO
    {
        public int Id { get; set; }
        public AuthorDTO Author { get; set; }
    }
}