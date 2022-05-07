using Articles.Models;
using Articles.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
namespace Articles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _articleRepository;
        public ArticleController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        //*get all : /api/article 
        [HttpGet("")]
        public async Task<IActionResult> GetAllArticles()
        {
            var articles = await _articleRepository.GetAllArticlesAsync();
            return Ok(articles);
        }

        //*get by id : /api/article/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticleById([FromRoute] int id)
        {
            var articles = await _articleRepository.GetArticleByIdAsync(id);
            return Ok(articles);
        }

        //*create : /api/article
        [HttpPost("")]
        public async Task<IActionResult> AddNewArticle([FromBody] ArticleModel articleModel)
        {
            var id = await _articleRepository.AddArticleAsync(articleModel);
            return CreatedAtAction(nameof(GetArticleById), new { id = id, controller = "article" }, id);
        }

        //*update : /api/article/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle([FromRoute] int id, [FromBody] ArticleModel articleModel)

        {
            await _articleRepository.UpdateArticleAsync(id, articleModel);
            return Ok();
        }

        //*update patch : /api/article/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateArticlePatch([FromRoute] int id, [FromBody] JsonPatchDocument articleModel)

        {
            await _articleRepository.UpdateArticlePatchAsync(id, articleModel);
            return Ok();
        }

        //*delete : /api/article/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle([FromRoute] int id)
        {
            await _articleRepository.DeleteArticleAsync(id);
            return Ok();
        }


    }
}