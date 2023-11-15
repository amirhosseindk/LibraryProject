using Domain.Entities;

namespace Application.Patterns
{
	public interface IInventoryReadRepository
    {
		Task<Inventory> GetByIdAsync(int bookId, CancellationToken cancellationToken = default);
		Task<IEnumerable<Inventory>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<Inventory> GetByNameAsync(string name, CancellationToken cancellationToken = default);
	}
}