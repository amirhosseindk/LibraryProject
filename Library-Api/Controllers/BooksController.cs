using Application.Commands.Book;
using Application.Query.Book;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ISender _sender;

        public BooksController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _sender.Send(new GetBooksQuery());

            if (books == null)
                return NotFound();

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _sender.Send(new GetBookQuery(id));

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var book = await _sender.Send(new GetBookQuery(0,name));

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookCommand request)
        {
            int newBookId = await _sender.Send(request);
            return CreatedAtAction(nameof(GetById), new { id = newBookId }, request);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateBookCommand request)
        {
            if (id != request.BookDto.ID)
                return BadRequest();

            await _sender.Send(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sender.Send(new DeleteBookCommand(id));
            return NoContent();
        }
    }
}