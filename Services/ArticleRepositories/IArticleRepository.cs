using Articles.Models.DTOs;
using Articles.Models.DTOs.ArticleImage;
using Articles.Models.DTOs.ArticleRequest;

namespace Articles.Services.ArticleRepositories
{
    public interface IArticleRepository
    {
        Task<object> GetArticles();
        Task<object> GetArticle(int id);
        Task<object> CreateArticle(ArticleCreateRequest request);
        Task<string> UpdateArticle(int id, ArticleUpdateRequest request);
        Task<string> DeleteArticle(int id);
    }
}