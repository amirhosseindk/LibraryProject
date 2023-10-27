using Application.DTO.Book;

namespace Application.UseCases.Book
{
    public interface IBookService
    {
        Task<int> CreateBook(BookCDto bookDto);
        Task UpdateBook(BookUDto bookDto);
        Task DeleteBook(int bookId);
        Task<BookRDto> GetBookDetails(int bookId);
        Task<BookRDto> GetBookDetails(string bookName);
        Task<IEnumerable<BookRDto>> ListBooks();
        Task BorrowBook(int bookId, int userId);
        Task ReturnBook(int bookId, int userId);
        Task PurchaseBook(int bookId, int userId);
    }
}