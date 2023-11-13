using Domain.Entities;

namespace Application.Patterns
{
    public interface IBookWriteRepository
    {
        Task AddAsync(Book book, CancellationToken cancellationToken = default);
        Task Update(Book book, CancellationToken cancellationToken = default);
        Task Delete(Book book, CancellationToken cancellationToken = default);
    }
}