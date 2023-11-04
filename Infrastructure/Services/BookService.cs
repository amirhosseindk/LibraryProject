using Application.DTO.Book;
using Application.Patterns;
using Application.UseCases.Book;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork, IBookRepository bookRepository)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
        }

        public async Task<int> CreateBook(BookCDto bookDto)
        {
            _unitOfWork.BeginTransaction();
            try
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
                _unitOfWork.Commit();

                return book.ID;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task UpdateBook(BookUDto bookDto)
        {
            _unitOfWork.BeginTransaction();
            try
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

                await _bookRepository.Update(existingBook);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task DeleteBook(int bookId)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var existingBook = await _bookRepository.GetByIdAsync(bookId);
                if (existingBook == null)
                {
                    throw new Exception("Book not found.");
                }

                _bookRepository.Delete(existingBook);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
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
    }
}