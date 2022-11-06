using System.ComponentModel.DataAnnotations;

namespace Articles.Models.DTOs.ArticleRequest
{
    public class ArticleCreateRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile Thumbnails { get; set; }
    }
}