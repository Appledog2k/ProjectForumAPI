using Articles.Controllers;
using Articles.GenericRepository.IRepository;
using Articles.Models.DTOs;
using AutoMapper;
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
        public Task<object> CreateArticle(Create_ArticleDTO articleDTO)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteArticle(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<object> GetArticle(int id)
        {
            var article = await _unitOfWork.Articles.GetAsync(query => query.Id == id, new List<string> { "Author" });
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

        public Task<string> UpdateArticle(int id, Create_ArticleDTO articleDTO)
        {
            throw new NotImplementedException();
        }
    }
}