using Domain.Entities;

namespace Application.Patterns
{
    public interface IBookReadRepository
    {
        Task<Book> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Book> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}