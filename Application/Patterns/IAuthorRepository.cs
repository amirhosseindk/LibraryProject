using Domain.Entities;

namespace Application.Patterns
{
    public interface IAuthorRepository
    {
        Task<Author> GetByIdAsync(int id);
        Task<Author> GetByNameAsync(string lastName);
        Task<IEnumerable<Author>> GetAllAsync();
        Task AddAsync(Author author);
        Task Update(Author author);
        Task Delete(Author author);
    }
}