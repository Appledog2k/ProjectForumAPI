using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Articles.Models.DTOs;
using Articles.Models.Response;
using Articles.Services.Resource;
using Articles.Models.DTOs.ArticleImage;
using Articles.Services.ArticleRepositories;
using Articles.Models.Data.AggregateArticles;
using AutoMapper;
using Articles.Services.ImageRepositories;
using System.Security.Claims;
using Articles.GenericRepository;
using Articles.Models.Data.AggregateUsers;
using Microsoft.AspNetCore.Identity;

namespace Project_Articles.Controllers
{
    [Route("forum/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApiUser> _userManager;
        public ArticleController(IArticleRepository articleRepository
        , IMapper mapper,
        IImageRepository imageRepository,
        IUnitOfWork unitOfWork,
        UserManager<ApiUser> userManager)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
            _imageRepository = imageRepository;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetArticles()
        {
            var articles = await _articleRepository.GetArticles();
            return Ok(new Response(Resource.GET_SUCCESS, null, articles));
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetArticle(int id)
        {
            var article = await _articleRepository.GetArticle(id);
            return Ok(new Response(Resource.GET_SUCCESS, new { id = id }, article));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateArticle([FromForm] ArticleCreateRequest request)
        {
            var article = _mapper.Map<Article>(request);
            article.CreatedDate = DateTime.Now;
            article.ImagePath = await _imageRepository.SaveFile(request.Thumbnails);
            article.ViewCount = 0;
            // Process relationship user
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApiUser user = await _userManager.FindByIdAsync(userId);
            article.AuthorName = $"{user.FirstName} {user.LastName}";

            await _unitOfWork.Articles.InsertAsync(article);
            await _unitOfWork.Save();
            return Ok(new Response(Resource.CREATE_SUCCESS, null, article.Title));
        }

        [HttpPut("{id:int}")]
        [Authorize]
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