using Domain.Entities;

namespace Application.Patterns
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(int id);
        Task<Book> GetByNameAsync(string name);
        Task<IEnumerable<Book>> GetAllAsync();
        Task AddAsync(Book book);
        Task Update(Book book);
        Task Delete(Book book);
    }
}