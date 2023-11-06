using Domain.Entities;

namespace Application.Patterns
{
    public interface IAuthorRepository
    {
        Task<Author> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Author> GetByNameAsync(string lastName, CancellationToken cancellationToken = default);
        Task<IEnumerable<Author>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Author author, CancellationToken cancellationToken = default);
        Task Update(Author author, CancellationToken cancellationToken = default);
        Task Delete(Author author, CancellationToken cancellationToken = default);
    }
}