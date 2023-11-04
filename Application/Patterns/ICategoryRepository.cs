using Domain.Entities;

namespace Application.Patterns
{
    public interface ICategoryRepository
    {
        Task<Category> GetByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task AddAsync(Category category);
        void Update(Category category);
        void Delete(Category category);
        Task<Category> GetByNameAsync(string name);
    }
}