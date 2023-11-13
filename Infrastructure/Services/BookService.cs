using Application.DTO.Book;
using Application.Patterns;
using Application.UseCases.Book;
using Domain.Entities;

namespace Infrastructure.Services
{
	public class BookService : IBookService
    {
        private readonly IBookReadRepository _bookReadRepository;
        private readonly IBookWriteRepository _bookWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork, IBookWriteRepository bookWriteRepository , IBookReadRepository bookReadRepository)
        {
            _unitOfWork = unitOfWork;
            _bookWriteRepository = bookWriteRepository;
            _bookReadRepository = bookReadRepository;
        }

        public async Task<int> CreateBook(BookCDto bookDto, CancellationToken cancellationToken)
        {
            _unitOfWork.BeginTransaction();
            try 
            {
                var existingBook = await _bookReadRepository.GetByNameAsync(bookDto.Name,cancellationToken);
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

                await _bookWriteRepository.AddAsync(book);
                _unitOfWork.Commit();

                return book.ID;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task UpdateBook(BookUDto bookDto, CancellationToken cancellationToken)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var existingBook = await _bookReadRepository.GetByIdAsync(bookDto.ID,cancellationToken);
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

                await _bookWriteRepository.Update(existingBook);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task DeleteBook(int bookId, CancellationToken cancellationToken)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var existingBook = await _bookReadRepository.GetByIdAsync(bookId,cancellationToken);
                if (existingBook == null)
                {
                    throw new Exception("Book not found.");
                }

                await _bookWriteRepository.Delete(existingBook);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<BookRDto> GetBookDetails(int bookId, CancellationToken cancellationToken)
        {
            var book = await _bookReadRepository.GetByIdAsync(bookId,cancellationToken);
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

        public async Task<BookRDto> GetBookDetails(string bookName, CancellationToken cancellationToken)
        {
            var book = await _bookReadRepository.GetByNameAsync(bookName, cancellationToken);
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

        public async Task<IEnumerable<BookRDto>> ListBooks(CancellationToken cancellationToken)
        {
            var books = await _bookReadRepository.GetAllAsync(cancellationToken);
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