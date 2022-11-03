namespace Articles.Data
{
    public class Article
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int ViewCount { get; set; }
        public DateTime Created { get; set; }
        public string? Content { get; set; }
        public string? UserId { get; set; }
        public ApiUser? ApiUser { get; set; }

        // Relationship Image
        public List<ImageArticle>? ImageArticles { get; set; }

    }
}