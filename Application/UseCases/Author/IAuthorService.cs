using Application.DTO.Author;

namespace Application.UseCases.Author
{
    public interface IAuthorService
    {
        Task<int> CreateAuthor(AuthorCDto authorDto);
        Task UpdateAuthor(AuthorUDto authorDto);
        Task DeleteAuthor(int authorId);
        Task<AuthorRDto> GetAuthorDetails(int authorId);
        Task<AuthorRDto> GetAuthorDetails(string authorName);
        Task<IEnumerable<AuthorRDto>> ListAuthors();
    }
}