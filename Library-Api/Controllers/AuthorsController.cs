using Application.Commands.Author;
using Application.Query.Author;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ISender _sender;

        public AuthorsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _sender.Send(new GetAuthorsQuery());

            if (authors == null)
            {
                return NotFound();
            }

            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var authors = await _sender.Send(new GetAuthorQuery(id));

            if (authors == null)
            {
                return NotFound();
            }

            return Ok(authors);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var authors = await _sender.Send(new GetAuthorQuery(0,name));

            if (authors == null)
            {
                return NotFound();
            }

            return Ok(authors);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorCommand request)
        {
            int newAuthorId = await _sender.Send(request);
            return CreatedAtAction(nameof(GetById), new { id = newAuthorId }, request);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateAuthorCommand request)
        {
            if (id != request.AuthorDto.ID)
                return BadRequest();

            await _sender.Send(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sender.Send(new DeleteAuthorCommand(id));
            return NoContent();
        }
    }
}