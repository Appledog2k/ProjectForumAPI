using Articles.GenericRepository.IRepository;
using Articles.Models.DTOs;
using AutoMapper;
using Project_Articles.Controllers;

namespace Articles.Services.DataHandling
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthorController> _logger;
        private readonly IMapper _mapper;

        public AuthorRepository(IUnitOfWork unitOfWork, ILogger<AuthorController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public Task<object> CreateAuthor(Create_AuthorDTO authorDTO)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteAuthor(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<object> GetAuthor(int id)
        {
            var author = await _unitOfWork.Authors.GetAsync(query => query.Id == id, new List<string> { "Articles" });
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

        public Task<string> UpdateAuthor(int id, Create_AuthorDTO authorDTO)
        {
            throw new NotImplementedException();
        }
    }
}