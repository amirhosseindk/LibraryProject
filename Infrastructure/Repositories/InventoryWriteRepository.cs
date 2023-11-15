using Application.Patterns;
using Domain.Entities;
using Infrastructure.Database;

namespace Infrastructure.Repositories
{
	public class InventoryWriteRepository : IInventoryWriteRepository
	{
		private readonly AppDbContext _context;

        public InventoryWriteRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;        
        }

		public async Task AddAsync(Inventory inventory, CancellationToken cancellationToken = default)
		{
			_context.Inventories.Add(inventory);
			await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		}

		public async void Update(Inventory inventory, CancellationToken cancellationToken = default)
		{
			_context.Inventories.Update(inventory);
			await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		}

		public async void Delete(Inventory inventory, CancellationToken cancellationToken = default)
		{
			_context.Inventories.Remove(inventory);
			await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		}
	}
}