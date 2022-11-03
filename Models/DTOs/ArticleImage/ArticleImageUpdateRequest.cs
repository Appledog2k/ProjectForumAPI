namespace Articles.Models.DTOs.ArticleImage
{
    public class ArticleImageUpdateRequest
    {
        public string? Caption { get; set; }
        public bool IsDefault { get; set; }
        public int ArticleId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}