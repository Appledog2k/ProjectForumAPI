namespace Articles.Models.DTOs.ArticleImage
{
    public class ArticleViewRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int ViewCount { get; set; }
        public string ImagePath { get; set; }
    }
}