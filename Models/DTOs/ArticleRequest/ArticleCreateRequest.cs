using System.ComponentModel.DataAnnotations;

namespace Articles.Models.DTOs.ArticleImage
{
    public class ArticleCreateRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile Thumbnails { get; set; }
    }
}