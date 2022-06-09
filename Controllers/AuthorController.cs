using Articles.Models.DTOs;
using Articles.Models.Response;
using Articles.Services.DataHandling;
using Articles.Services.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project_Articles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            return Ok(new Response(Resource.GET_SUCCESS, authors));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await _authorRepository.GetAuthor(id);
            return Ok(new Response(Resource.GET_SUCCESS, new { id = id }, author));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] Create_AuthorDTO authorDTO)
        {
            var result = await _authorRepository.CreateAuthor(authorDTO);
            return Ok(new Response(Resource.CREATE_SUCCESS, null, result));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] Update_AuthorDTO authorDTO)
        {
            var result = await _authorRepository.UpdateAuthor(id, authorDTO);
            return Ok(new Response(result));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await _authorRepository.DeleteAuthor(id);
            return Ok(new Response(result));
        }
    }
}