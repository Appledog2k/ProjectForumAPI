using Microsoft.AspNetCore.Mvc;
using Articles.Services.DataHandling;

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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetArticle(int id)
        {
            var article = await _articleRepository.GetArticle(id);

            // ResourceException
            return Ok(article);
        }


    }
}