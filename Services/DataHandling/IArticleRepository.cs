using Articles.Models.DTOs;

namespace Articles.Services.DataHandling
{
    public interface IArticleRepository
    {
        Task<object> GetArticles();
        Task<object> GetArticle(int id);
        Task<object> CreateArticle(Create_ArticleDTO articleDTO);
        Task<string> UpdateArticle(int id, Create_ArticleDTO articleDTO);
        Task<string> DeleteArticle(int id);
    }
}