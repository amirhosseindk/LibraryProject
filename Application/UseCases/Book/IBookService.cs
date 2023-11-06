using Application.DTO.Book;

namespace Application.UseCases.Book
{
    public interface IBookService
    {
        Task<int> CreateBook(BookCDto bookDto, CancellationToken cancellationToken = default);
        Task UpdateBook(BookUDto bookDto, CancellationToken cancellationToken = default);
        Task DeleteBook(int bookId, CancellationToken cancellationToken = default);
        Task<BookRDto> GetBookDetails(int bookId, CancellationToken cancellationToken = default);
        Task<BookRDto> GetBookDetails(string bookName, CancellationToken cancellationToken = default);
        Task<IEnumerable<BookRDto>> ListBooks(CancellationToken cancellationToken = default);
    }
}