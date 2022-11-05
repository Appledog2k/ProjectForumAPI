
using Articles.GenericRepository;
using Articles.Models;
using Articles.Models.Data.AggregateArticles;
using Articles.Models.Data.DbContext;
using Articles.Models.DTOs;
using Articles.Models.DTOs.ArticleImage;
using Articles.Services.ImageRepositories;
using Articles.Services.StorageServices;
using AutoMapper;
using Project_Articles.Controllers;

namespace Articles.Services.ArticleRepositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ArticleController> _logger;
        private readonly IStorageService _storageService;
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly IImageRepository _imageRepository;

        public ArticleRepository(IUnitOfWork unitOfWork, ILogger<ArticleController> logger, IMapper mapper,
        DatabaseContext context,
        IStorageService storageService,
         IImageRepository imageRepository)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _context = context;
            _storageService = storageService;
            _imageRepository = imageRepository;
        }
        public async Task<object> CreateArticle(ArticleCreateRequest request)
        {
            var article = _mapper.Map<Article>(request);
            article.ImagePath = await _imageRepository.SaveFile(request.Thumbnails);
            article.ViewCount = 0;
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
            var article = await _unitOfWork.Articles.GetAsync(q => q.Id == id);
            if (article == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteArticle)}");
                throw new BusinessException(Resource.Resource.NOT_DATA);
            }
            await _unitOfWork.Articles.DeleteAsync(id);
            await _unitOfWork.Save();
            return Resource.Resource.DELETE_SUCCESS;
        }
        public async Task<object> GetArticle(int id)
        {
            var article = await _unitOfWork.Articles.GetAsync(query => query.Id == id);
            var result = _mapper.Map<ArticleViewRequest>(article);
            return new
            {
                result
            };
        }
        public async Task<object> GetArticles()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync();
            var results = _mapper.Map<IList<ArticleViewRequest>>(articles);
            return new
            {
                results
            };
        }
        public async Task<string> UpdateArticle(int id, ArticleUpdateRequest request)
        {
            var article = await _unitOfWork.Articles.GetAsync(q => q.Id == id);
            if (article == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateArticle)}");
                throw new BusinessException(Resource.Resource.NOT_DATA);
            }
            article = _mapper.Map<Article>(request);
            article.ImagePath = await _imageRepository.SaveFile(request.Thumbnails);
            _unitOfWork.Articles.Update(article);
            await _unitOfWork.Save();
            return Resource.Resource.UPDATE_SUCCESS;
        }

    }
}