using Domain.Entities;

namespace Application.Patterns
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(int id , CancellationToken cancellationToken = default);
        Task<Book> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Book book, CancellationToken cancellationToken = default);
        Task Update(Book book, CancellationToken cancellationToken = default);
        Task Delete(Book book, CancellationToken cancellationToken = default);
    }
}