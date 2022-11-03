namespace Articles.Models.DTOs.ArticleImage
{
    public class ArticleImageCreateRequest
    {
        public string? Caption { get; set; }
        public bool IsDefault { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}