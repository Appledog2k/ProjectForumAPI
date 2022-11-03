using System.Net.Http.Headers;
using System.Security.Claims;
using Articles.Data;
using Articles.GenericRepository;
using Articles.Models;
using Articles.Models.DTOs;
using Articles.Models.DTOs.ArticleImage;
using Articles.Services.StorageService;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Articles.Controllers;

namespace Articles.Services.DataHandling
{
    public interface IArticleRepository
    {
        Task<object> GetArticles();
        Task<object> GetArticle(int id);
        Task<object> CreateArticle(Create_ArticleDTO articleDTO);
        Task<string> UpdateArticle(int id, Update_ArticleDTO articleDTO);
        Task<string> DeleteArticle(int id);
        Task<int> AddImage(int articleId, ArticleImageCreateRequest articleImage);
        Task<int> RemoveImage(int imageId);
        Task<int> UpdateImage(int imageId, ArticleImageUpdateRequest articleImage);
        Task<ArticleImageViewModel> GetImageById(int imageId);

        Task<List<ArticleImageViewModel>> GetListImages(int productId);
    }
    public class ArticleRepository : IArticleRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ArticleController> _logger;
        private readonly IStorageService _storageService;
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;

        public ArticleRepository(IUnitOfWork unitOfWork, ILogger<ArticleController> logger, IMapper mapper,
        DatabaseContext context,
        IStorageService storageService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _context = context;
            _storageService = storageService;
        }
        public async Task<object> CreateArticle(Create_ArticleDTO articleDTO)
        {
            var article = _mapper.Map<Article>(articleDTO);
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
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

        public async Task<string> UpdateArticle(int id, Update_ArticleDTO articleDTO)
        {
            var article = await _unitOfWork.Articles.GetAsync(q => q.Id == id);
            if (article == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateArticle)}");
                throw new BusinessException(Resource.Resource.NOT_DATA);
            }
            _mapper.Map(articleDTO, article);
            _unitOfWork.Articles.Update(article);
            await _unitOfWork.Save();
            return Resource.Resource.UPDATE_SUCCESS;
        }
        public async Task<int> AddImage(int articleId, ArticleImageCreateRequest image)
        {
            var articleImage = new ImageArticle()
            {
                Caption = image.Caption,
                DateCreated = DateTime.Now,
                ArticleId = articleId,
            };

            if (image.ImageFile != null)
            {
                articleImage.ImagePath = await this.SaveFile(image.ImageFile);
            };
            _context.ImageArticles.Add(articleImage);
            await _context.SaveChangesAsync();
            return articleImage.Id;
        }

        public async Task<int> RemoveImage(int imageId)
        {
            var articleImage = await _context.ImageArticles.FindAsync(imageId);
            if (articleImage == null)
            {
                throw new NotImplementedException();
            }
            _context.ImageArticles.Remove(articleImage);
            return await _context.SaveChangesAsync();

        }
        public async Task<int> UpdateImage(int imageId, ArticleImageUpdateRequest image)
        {
            var articleImage = await _context.ImageArticles.FindAsync(imageId);

            if (articleImage == null)
            {
                throw new NotImplementedException();
            };
            if (image.ImageFile != null)
            {
                articleImage.ImagePath = await this.SaveFile(image.ImageFile);
            };
            _context.ImageArticles.Update(articleImage);
            return await _context.SaveChangesAsync();
        }
        public async Task<ArticleImageViewModel> GetImageById(int imageId)
        {
            var image = await _context.ImageArticles.FindAsync(imageId);
            if (image == null)
            {
                throw new NotImplementedException();
            }
            var viewModel = new ArticleImageViewModel()
            {
                Caption = image.Caption,
                DateCreated = image.DateCreated,
                Id = image.Id,
                ImagePath = image.ImagePath,
            };
            return viewModel;
        }

        public async Task<List<ArticleImageViewModel>> GetListImages(int articleId)
        {
            return await _context.ImageArticles.Where(x => x.ArticleId == articleId).Select(x => new ArticleImageViewModel()
            {
                Caption = x.Caption,
                DateCreated = x.DateCreated,
                Id = x.Id,
                ImagePath = x.ImagePath,
            }).ToListAsync();
        }


        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }


    }
}