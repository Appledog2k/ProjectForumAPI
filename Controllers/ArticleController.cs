using Microsoft.AspNetCore.Mvc;
using Articles.Services.DataHandling;
using Microsoft.AspNetCore.Authorization;
using Articles.Models.DTOs;
using Articles.Models.Response;
using Articles.Services.Resource;
using Articles.Models.DTOs.ArticleImage;
using System.Security.Claims;

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
        [HttpGet("/currentUser")]
        public IActionResult GetCurrentUserAsync()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(username);
        }


        // todo : get articles
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetArticles()
        {
            var articles = await _articleRepository.GetArticles();
            return Ok(new Response(Resource.GET_SUCCESS, articles));
        }

        // todo : get article by id
        [HttpGet("{id:int}")]
        [AllowAnonymous]
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
        [HttpPost("{articleId}/images")]
        public async Task<IActionResult> CreateImageArticle(int articleId, [FromForm] ArticleImageCreateRequest request)
        {
            var imageId = await _articleRepository.AddImage(articleId, request);
            if (imageId == 0)
            {
                return BadRequest();
            }

            var image = await _articleRepository.GetImageById(imageId);
            return Ok(image.Caption);
        }
        [HttpPut("{articleId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImageArticle(int imageId, [FromForm] ArticleImageUpdateRequest request)
        {
            var result = await _articleRepository.UpdateImage(imageId, request);
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete("{articleId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImageArticle(int imageId, [FromForm] ArticleImageUpdateRequest request)
        {
            var result = await _articleRepository.RemoveImage(imageId);
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpGet("{articleId:int}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int articleId, int imageId)
        {
            var image = await _articleRepository.GetImageById(articleId);
            return Ok(image);
        }


    }
}