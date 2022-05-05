using Articles.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Articles.Repository
{
    public interface IArticleRepository
    {
        Task<List<ArticleModel>> GetAllArticlesAsync();
        Task<ArticleModel> GetArticleByIdAsync(int articleId);
        Task<int> AddArticleAsync(ArticleModel articleModel);
        Task UpdateArticleAsync(int articleId, ArticleModel articleModel);
        Task UpdateArticlePatchAsync(int articleId, JsonPatchDocument articleModel);
        Task DeleteArticleAsync(int articleId);

    }
}