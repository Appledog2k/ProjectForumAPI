using System.ComponentModel.DataAnnotations;

namespace Articles.Models.DTOs.ArticleImage
{
    public class ArticleUpdateRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int ViewCount { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile Thumbnails { get; set; }
    }
}