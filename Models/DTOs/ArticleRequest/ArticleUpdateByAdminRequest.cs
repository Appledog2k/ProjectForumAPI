using System.ComponentModel.DataAnnotations;

namespace Articles.Models.DTOs.ArticleImage
{
    public class ArticleUpdateByAdminRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int Category { get; set; }
        public int ViewCount { get; set; }
        public bool IsActive { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile Thumbnails { get; set; }
    }
}