using Articles.Data;
using Articles.GenericRepository;
using Articles.Models;
using Articles.Models.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project_Articles.Controllers;

namespace Articles.Services.DataHandling
{
    public interface IAuthorRepository
    {
        Task<object> GetAuthors();
        Task<object> GetAuthor(int id);
        Task<object> CreateAuthor(Create_AuthorDTO authorDTO);
        Task<string> UpdateAuthor(int id, Update_AuthorDTO authorDTO);
        Task<string> DeleteAuthor(int id);
    }
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthorController> _logger;
        private readonly IMapper _mapper;

        public AuthorRepository(IUnitOfWork unitOfWork,
                                ILogger<AuthorController> logger,
                                IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<object> CreateAuthor(Create_AuthorDTO authorDTO)
        {
            var author = _mapper.Map<Author>(authorDTO);
            await _unitOfWork.Authors.InsertAsync(author);
            await _unitOfWork.Save();
            return new
            {
                id = author.Id,
                author
            };
        }

        public async Task<string> DeleteAuthor(int id)
        {
            var author = await _unitOfWork.Authors.GetAsync(q => q.Id == id);
            if (author == null || id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteAuthor)}");
                throw new BusinessException(Resource.Resource.NOT_DATA);
            }
            await _unitOfWork.Authors.DeleteAsync(id);
            await _unitOfWork.Save();
            return Resource.Resource.DELETE_SUCCESS;
        }

        public async Task<object> GetAuthor(int id)
        {
            var author = await _unitOfWork.Authors.GetAsync(query => query.Id == id, q => q.Include(x => x.Articles));
            var result = _mapper.Map<AuthorDTO>(author);
            return new
            {
                result
            };
        }

        public async Task<object> GetAuthors()
        {
            var authors = await _unitOfWork.Authors.GetAllAsync();
            var results = _mapper.Map<IList<AuthorDTO>>(authors);
            return new
            {
                results
            };
        }

        public async Task<string> UpdateAuthor(int id, Update_AuthorDTO authorDTO)
        {
            var author = await _unitOfWork.Authors.GetAsync(q => q.Id == id);
            if (author == null)
            {
                throw new BusinessException(Resource.Resource.NOT_DATA);
            }

            _mapper.Map(authorDTO, author);
            _unitOfWork.Authors.Update(author);
            await _unitOfWork.Save();
            return Resource.Resource.UPDATE_SUCCESS;
        }
    }
}