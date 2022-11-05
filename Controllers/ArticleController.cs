using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Articles.Models.DTOs;
using Articles.Models.Response;
using Articles.Services.Resource;
using Articles.Models.DTOs.ArticleImage;
using Articles.Services.ArticleRepositories;

namespace Project_Articles.Controllers
{
    [Route("forum/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _articleRepository;
        public ArticleController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetArticles()
        {
            var articles = await _articleRepository.GetArticles();
            return Ok(new Response(Resource.GET_SUCCESS, articles));
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetArticle(int id)
        {
            var article = await _articleRepository.GetArticle(id);
            return Ok(new Response(Resource.GET_SUCCESS, new { id = id }, article));
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle([FromForm] ArticleCreateRequest request)
        {
            var result = await _articleRepository.CreateArticle(request);
            return Ok(new Response(Resource.CREATE_SUCCESS, null, result));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateArticle(int id, [FromForm] ArticleUpdateRequest request)
        {
            var result = await _articleRepository.UpdateArticle(id, request);
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