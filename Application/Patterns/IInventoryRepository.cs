using Domain.Entities;

namespace Application.Patterns
{
    public interface IInventoryRepository
    {
        Task<Inventory> GetByIdAsync(int bookId);
        Task<IEnumerable<Inventory>> GetAllAsync();
        Task AddAsync(Inventory inventory);
        void Update(Inventory inventory);
        void Delete(Inventory inventory);
        Task<Inventory> GetByNameAsync(string name);
    }
}