using Articles.Data;
using Articles.GenericRepository.IRepository;
using Articles.Models.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project_Articles.Controllers;

namespace Articles.Services.DataHandling
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ArticleController> _logger;
        private readonly IMapper _mapper;

        public ArticleRepository(IUnitOfWork unitOfWork, ILogger<ArticleController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<object> CreateArticle(Create_ArticleDTO articleDTO)
        {
            var article = _mapper.Map<Article>(articleDTO);
            await _unitOfWork.Articles.InsertAsync(article);
            await _unitOfWork.Save();
            return new
            {
                id = article.Id,
                article
            };
        }

        public async Task<string> DeleteArticle(int id)
        {
            var article = await _unitOfWork.Articles.GetAsync(pt => pt.Id == id);
            if (article == null)
            {
                return "Article not found";
            }

            await _unitOfWork.Articles.DeleteAsync(id);
            await _unitOfWork.Save();
            return "Article deleted";
        }

        public async Task<object> GetArticle(int id)
        {
            var article = await _unitOfWork.Articles.GetAsync(query => query.Id == id, q => q.Include(x => x.Author));
            var result = _mapper.Map<ArticleDTO>(article);
            return new
            {
                result
            };
        }

        public async Task<object> GetArticles()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync();
            var results = _mapper.Map<IList<ArticleDTO>>(articles);
            return new
            {
                results
            };
        }

        public async Task<string> UpdateArticle(int id, Create_ArticleDTO articleDTO)
        {
            var article = await _unitOfWork.Articles.GetAsync(pt => pt.Id == id);
            if (article == null)
            {
                return "Article not found";
            }
            _mapper.Map(articleDTO, article);
            _unitOfWork.Articles.Update(article);
            await _unitOfWork.Save();
            return "Article updated successfully";
        }
    }
}