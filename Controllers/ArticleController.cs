using Microsoft.AspNetCore.Mvc;
using Articles.Services.DataHandling;
using Microsoft.AspNetCore.Authorization;
using Articles.Models.DTOs;
using Articles.Models.Response;
using Articles.Services.Resource;

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

        // todo : get articles
        [HttpGet]
        public async Task<IActionResult> GetArticles()
        {
            var articles = await _articleRepository.GetArticles();
            return Ok(new Response(Resource.GET_SUCCESS, articles));
        }

        // todo : get article by id
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetArticle(int id)
        {
            var article = await _articleRepository.GetArticle(id);
            return Ok(new Response(Resource.GET_SUCCESS, new { id = id }, article));
        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult> CreateArticle([FromBody] Create_ArticleDTO articleDTO)
        {
            var result = await _articleRepository.CreateArticle(articleDTO);
            return Ok(new Response(Resource.CREATE_SUCCESS, null, result));
        }

        [HttpPut]
        [Authorize]

        public async Task<IActionResult> UpdateArticle(int id, [FromBody] Update_ArticleDTO articleDTO)
        {
            var result = await _articleRepository.UpdateArticle(id, articleDTO);
            return Ok(new Response(result));
        }


        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var result = await _articleRepository.DeleteArticle(id);
            return Ok(new Response(result));
        }

    }
}