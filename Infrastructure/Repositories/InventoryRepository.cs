using Application.Patterns;
using Dapper;
using Domain.Entities;
using Infrastructure.Database;

namespace Infrastructure.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly DbSession _session;

        public InventoryRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<Inventory> GetByIdAsync(int bookId)
        {
            var query = "SELECT * FROM Inventories WHERE BookID = @BookID";
            return await _session.Connection.QuerySingleOrDefaultAsync<Inventory>(query, new { BookID = bookId }, _session.Transaction);
        }

        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            var query = "SELECT * FROM Inventories";
            return await _session.Connection.QueryAsync<Inventory>(query, _session.Transaction);
        }

        public async Task AddAsync(Inventory inventory)
        {
            var query = "INSERT INTO Inventories (BookID, QuantityAvailable, QuantitySold, QuantityBorrowed) VALUES (@BookID, @QuantityAvailable, @QuantitySold, @QuantityBorrowed)";
            await _session.Connection.ExecuteAsync(query, inventory, _session.Transaction);
        }

        public void Update(Inventory inventory)
        {
            var query = "UPDATE Inventories SET QuantityAvailable = @QuantityAvailable, QuantitySold = @QuantitySold, QuantityBorrowed = @QuantityBorrowed WHERE BookID = @BookID";
            _session.Connection.Execute(query, inventory, _session.Transaction);
        }

        public void Delete(Inventory inventory)
        {
            var query = "DELETE FROM Inventories WHERE BookID = @BookID";
            _session.Connection.Execute(query, new { BookID = inventory.BookId }, _session.Transaction);
        }

        public async Task<Inventory> GetByNameAsync(string name)
        {
            var query = @"SELECT i.* 
                  FROM Inventories i
                  JOIN Books b ON i.BookID = b.ID
                  WHERE b.Name = @Name";
            return await _session.Connection.QuerySingleOrDefaultAsync<Inventory>(query, new { Name = name }, _session.Transaction);
        }
    }
}