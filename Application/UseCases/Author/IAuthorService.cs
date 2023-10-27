using Application.DTO.Author;
using Application.DTO.Book;

namespace Application.UseCases.Author
{
    public interface IAuthorService
    {
        Task CreateAuthor(AuthorCDto authorDto);
        Task UpdateAuthor(AuthorUDto authorDto);
        Task DeleteAuthor(int authorId);
        Task<AuthorRDto> GetAuthorDetails(int authorId);
        Task<IEnumerable<AuthorRDto>> ListAuthors();
        Task<IEnumerable<BookRDto>> ListBooksByAuthor(int authorId);
    }
}