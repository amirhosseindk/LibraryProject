using Domain.Entities;

namespace Application.Patterns
{
    public interface ICategoryRepository
    {
        Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Category category, CancellationToken cancellationToken = default);
		Task UpdateAsync(Category category, CancellationToken cancellationToken = default);
		Task DeleteAsync(Category category, CancellationToken cancellationToken = default);
        Task<Category> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}