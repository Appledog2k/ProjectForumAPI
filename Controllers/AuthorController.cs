using Articles.Models.DTOs;
using Articles.Services.DataHandling;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project_Articles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _authorRepository.GetAuthors();

            // ResourceException
            return Ok(authors);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await _authorRepository.GetAuthor(id);

            // ResourceException
            return Ok(author);
        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult> CreateAuthor([FromBody] Create_AuthorDTO authorDTO)
        {
            var result = await _authorRepository.CreateAuthor(authorDTO);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] Create_AuthorDTO authorDTO)
        {
            var result = await _authorRepository.UpdateAuthor(id, authorDTO);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await _authorRepository.DeleteAuthor(id);
            return Ok(result);
        }


    }
}