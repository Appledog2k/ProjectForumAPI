namespace Articles.Data
{

    public class ImageArticle
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string? ImagePath { get; set; }
        public string? Caption { get; set; }

        public int FileSize { get; set; }

        public int ArticleId { get; set; }
        public Article? Article { get; set; }
    }

}