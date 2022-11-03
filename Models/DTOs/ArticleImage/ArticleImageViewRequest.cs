namespace Articles.Models.DTOs.ArticleImage
{
    public class ArticleImageViewModel
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string? ImagePath { get; set; }
        public string? Caption { get; set; }
        public bool IsDefault { get; set; }
        public int FileSize { get; set; }
    }
}