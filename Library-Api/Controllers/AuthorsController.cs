using Application.DTO.Author;
using Application.UseCases.Author;
using Microsoft.AspNetCore.Mvc;

namespace Library_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorService.ListAuthors();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _authorService.GetAuthorDetails(id);
            if (author == null)
                return NotFound();

            return Ok(author);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var author = await _authorService.GetAuthorDetails(name);
            if (author == null)
                return NotFound();

            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuthorCDto authorCDto)
        {
            int newAuthorId = await _authorService.CreateAuthor(authorCDto);
            return CreatedAtAction(nameof(GetById), new { id = newAuthorId }, authorCDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AuthorUDto authorUDto)
        {
            if (id != authorUDto.ID)
                return BadRequest();

            await _authorService.UpdateAuthor(authorUDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.DeleteAuthor(id);
            return NoContent();
        }
    }
}