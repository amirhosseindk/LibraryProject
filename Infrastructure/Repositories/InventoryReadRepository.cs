using Application.Patterns;
using Dapper;
using Domain.Entities;
using Infrastructure.Database;

namespace Infrastructure.Repositories
{
	public class InventoryReadRepository : IInventoryReadRepository
	{
		private readonly AppDbSession _session;
		
		public InventoryReadRepository(AppDbSession session)
		{
			_session = session;
		}

		public async Task<Inventory> GetByIdAsync(int bookId, CancellationToken cancellationToken = default)
		{
			var query = "SELECT * FROM Inventories WHERE BookID = @BookID";
			return await _session.Connection.QuerySingleOrDefaultAsync<Inventory>(query, new { BookID = bookId }, _session.Transaction);
		}

		public async Task<IEnumerable<Inventory>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var query = "SELECT * FROM Inventories";
			return await _session.Connection.QueryAsync<Inventory>(query, _session.Transaction);
		}

		public async Task<Inventory> GetByNameAsync(string name, CancellationToken cancellationToken = default)
		{
			var query = @"SELECT i.* 
                  FROM Inventories i
                  JOIN Books b ON i.BookID = b.ID
                  WHERE b.Name = @Name";
			return await _session.Connection.QuerySingleOrDefaultAsync<Inventory>(query, new { Name = name }, _session.Transaction);
		}
	}
}