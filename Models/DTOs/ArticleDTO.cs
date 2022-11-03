
using Articles.Models.Data.AggregateImages;

namespace Articles.Models.DTOs
{
    public class Create_ArticleDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int ViewCount { get; set; }
        public IFormFile ThumbnailImage { get; set; }

    }

    public class Update_ArticleDTO : Create_ArticleDTO
    {
    }

    public class ArticleDTO : Create_ArticleDTO
    {
        public int Id { get; set; }
        public List<ImageArticle> ImageArticles { get; set; }
    }
}