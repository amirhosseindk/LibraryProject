using Application.DTO.Author;

namespace Application.UseCases.Author
{
    public interface IAuthorService
    {
        Task<int> CreateAuthor(AuthorCDto authorDto,CancellationToken cancellationToken = default);
        Task UpdateAuthor(AuthorUDto authorDto, CancellationToken cancellationToken = default);
        Task DeleteAuthor(int authorId, CancellationToken cancellationToken = default);
        Task<AuthorRDto> GetAuthorDetails(int authorId, CancellationToken cancellationToken = default);
        Task<AuthorRDto> GetAuthorDetails(string authorName, CancellationToken cancellationToken = default);
        Task<IEnumerable<AuthorRDto>> ListAuthors(CancellationToken cancellationToken = default);
    }
}