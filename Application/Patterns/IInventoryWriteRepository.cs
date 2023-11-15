using Domain.Entities;

namespace Application.Patterns
{
	public interface IInventoryWriteRepository
    {
		Task AddAsync(Inventory inventory, CancellationToken cancellationToken = default);
		void Update(Inventory inventory, CancellationToken cancellationToken = default);
		void Delete(Inventory inventory, CancellationToken cancellationToken = default);
	}
}