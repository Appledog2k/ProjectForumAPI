using Articles.Models.DTOs;
using Articles.Models.DTOs.ArticleImage;

namespace Articles.Services.ArticleRepositories
{
    public interface IArticleRepository
    {
        Task<object> GetArticles();
        Task<object> GetArticle(int id);
        Task<object> CreateArticle(Create_ArticleDTO articleDTO);
        Task<string> UpdateArticle(int id, Update_ArticleDTO articleDTO);
        Task<string> DeleteArticle(int id);
        Task<int> AddImage(int articleId, ArticleImageCreateRequest articleImage);
        Task<int> RemoveImage(int imageId);
        Task<int> UpdateImage(int imageId, ArticleImageUpdateRequest articleImage);
        Task<ArticleImageViewModel> GetImageById(int imageId);

        Task<List<ArticleImageViewModel>> GetListImages(int productId);
    }
}