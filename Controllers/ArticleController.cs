using Microsoft.AspNetCore.Mvc;
using Articles.Services.DataHandling;
using Microsoft.AspNetCore.Authorization;
using Articles.Models.DTOs;

namespace Project_Articles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _articleRepository;
        public ArticleController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetArticles()
        {
            try
            {
                var articles = await _articleRepository.GetArticles();
                return Ok(articles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetArticle(int id)
        {
            var article = await _articleRepository.GetArticle(id);

            // ResourceException
            return Ok(article);
        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult> CreateArticle([FromBody] Create_ArticleDTO articleDTO)
        {
            var result = await _articleRepository.CreateArticle(articleDTO);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateArticle(int id, [FromBody] Create_ArticleDTO articleDTO)
        {
            var result = await _articleRepository.UpdateArticle(id, articleDTO);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var result = await _articleRepository.DeleteArticle(id);
            return Ok(result);
        }

    }
}