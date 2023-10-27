using Application.DTO.Book;
using Application.Patterns;
using Application.UseCases.Book;
using Application.UseCases.Inventory;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInventoryService _inventoryService;

        public BookService(IUnitOfWork unitOfWork, IInventoryService inventoryService)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = _unitOfWork.BookRepository;
            _inventoryService = inventoryService;
        }

        public async Task CreateBook(BookCDto bookDto)
        {
            var existingBook = await _bookRepository.GetByNameAsync(bookDto.Name);
            if (existingBook != null)
            {
                throw new Exception("Book with this name already exists.");
            }

            var book = new Book
            {
                Name = bookDto.Name,
                Author = new Author { ID = bookDto.AuthorId },
                Category = new Category { ID = bookDto.CategoryId },
                Publisher = bookDto.Publisher,
                Price = bookDto.Price,
                Summary = bookDto.Summary,
                PublishYear = bookDto.PublishYear
            };

            await _bookRepository.AddAsync(book);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateBook(BookUDto bookDto)
        {
            var existingBook = await _bookRepository.GetByIdAsync(bookDto.ID);
            if (existingBook == null)
            {
                throw new Exception("Book not found.");
            }

            existingBook.Name = bookDto.Name;
            existingBook.Author = new Author { ID = bookDto.AuthorId };
            existingBook.Category = new Category { ID = bookDto.CategoryId };
            existingBook.Publisher = bookDto.Publisher;
            existingBook.Price = bookDto.Price;
            existingBook.Summary = bookDto.Summary;
            existingBook.PublishYear = bookDto.PublishYear;

            _bookRepository.Update(existingBook);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteBook(int bookId)
        {
            var existingBook = await _bookRepository.GetByIdAsync(bookId);
            if (existingBook == null)
            {
                throw new Exception("Book not found.");
            }

            _bookRepository.Delete(existingBook);
            await _unitOfWork.SaveAsync();
        }

        public async Task<BookRDto> GetBookDetails(int bookId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null)
            {
                throw new Exception("Book not found.");
            }

            return new BookRDto
            {
                ID = book.ID,
                Name = book.Name,
                AuthorName = book.Author?.FirstName + " " + book.Author?.LastName,
                CategoryName = book.Category?.Name,
                Publisher = book.Publisher,
                Price = book.Price,
                Score = book.Score,
                Summary = book.Summary,
                PublishYear = book.PublishYear
            };
        }

        public async Task<BookRDto> GetBookDetails(string bookName)
        {
            var book = await _bookRepository.GetByNameAsync(bookName);
            if (book == null)
            {
                throw new Exception("Book not found.");
            }

            return new BookRDto
            {
                ID = book.ID,
                Name = book.Name,
                AuthorName = book.Author?.FirstName + " " + book.Author?.LastName,
                CategoryName = book.Category?.Name,
                Publisher = book.Publisher,
                Price = book.Price,
                Score = book.Score,
                Summary = book.Summary,
                PublishYear = book.PublishYear
            };
        }

        public async Task<IEnumerable<BookRDto>> ListBooks()
        {
            var books = await _bookRepository.GetAllAsync();
            return books.Select(b => new BookRDto
            {
                ID = b.ID,
                Name = b.Name,
                AuthorName = b.Author?.FirstName + " " + b.Author?.LastName,
                CategoryName = b.Category?.Name,
                Publisher = b.Publisher,
                Price = b.Price,
                Score = b.Score,
                Summary = b.Summary,
                PublishYear = b.PublishYear
            });
        }

        public async Task BorrowBook(int bookId, int userId)
        {
            var inventory = await _inventoryService.GetInventoryDetailsByBookId(bookId);
            if (inventory.QuantityAvailable <= 0)
            {
                throw new Exception("No books available for borrowing.");
            }

            await _inventoryService.DecreaseQuantityAvailable(bookId);
            await _inventoryService.IncreaseQuantityBorrowed(bookId);
        }

        public async Task ReturnBook(int bookId, int userId)
        {
            await _inventoryService.IncreaseQuantityAvailable(bookId);
            await _inventoryService.DecreaseQuantityBorrowed(bookId);
        }

        public async Task PurchaseBook(int bookId, int userId)
        {
            var inventory = await _inventoryService.GetInventoryDetailsByBookId(bookId);
            if (inventory.QuantityAvailable <= 0)
            {
                throw new Exception("No books available for purchase.");
            }

            await _inventoryService.DecreaseQuantityAvailable(bookId);
            await _inventoryService.IncreaseQuantitySold(bookId);
        }
    }
}