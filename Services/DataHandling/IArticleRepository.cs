using Articles.Models.DTOs;

namespace Articles.Services.DataHandling
{
    public interface IArticleRepository
    {
        Task<object> GetArticles();
        Task<object> GetArticle(int id);
        Task<object> CreateArticle(Create_ArticleDTO articleDTO);
        Task<string> UpdateArticle(int id, Update_ArticleDTO articleDTO);
        Task<string> DeleteArticle(int id);
    }
}