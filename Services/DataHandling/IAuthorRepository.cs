using Articles.Models.DTOs;

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
}