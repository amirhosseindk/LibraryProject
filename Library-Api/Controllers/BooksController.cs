using Application.DTO.Book;
using Application.UseCases.Book;
using Microsoft.AspNetCore.Mvc;

namespace Library_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.ListBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetBookDetails(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var book = await _bookService.GetBookDetails(name);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookCDto bookDto)
        {
            int newBookId = await _bookService.CreateBook(bookDto);
            return CreatedAtAction(nameof(GetById), new { id = newBookId }, bookDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookUDto bookDto)
        {
            if (id != bookDto.ID)
                return BadRequest();

            await _bookService.UpdateBook(bookDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteBook(id);
            return NoContent();
        }

        [HttpPost("{id}/borrow/{userId}")]
        public async Task<IActionResult> Borrow(int id, int userId)
        {
            await _bookService.BorrowBook(id, userId);
            return NoContent();
        }

        [HttpPost("{id}/return/{userId}")]
        public async Task<IActionResult> Return(int id, int userId)
        {
            await _bookService.ReturnBook(id, userId);
            return NoContent();

        }

        [HttpPost("{id}/purchase/{userId}")]
        public async Task<IActionResult> Purchase(int id, int userId)
        {
            await _bookService.PurchaseBook(id, userId);
            return NoContent();
        }
    }
}