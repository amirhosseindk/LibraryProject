using Domain.Entities;

namespace Application.Patterns
{
    public interface IInventoryRepository
    {
        Task<Inventory> GetByIdAsync(int bookId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Inventory>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(Inventory inventory, CancellationToken cancellationToken);
        void Update(Inventory inventory, CancellationToken cancellationToken);
        void Delete(Inventory inventory, CancellationToken cancellationToken);
        Task<Inventory> GetByNameAsync(string name, CancellationToken cancellationToken);
    }
}