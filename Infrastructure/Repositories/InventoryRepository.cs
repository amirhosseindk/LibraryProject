using Application.Patterns;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Repositories
{
    public class InventoryRepository : IRepository<Inventory>
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public InventoryRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public async Task<Inventory> GetByIdAsync(int bookId)
        {
            var query = "SELECT * FROM Inventories WHERE BookID = @BookID";
            return await _connection.QuerySingleOrDefaultAsync<Inventory>(query, new { BookID = bookId });
        }

        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            var query = "SELECT * FROM Inventories";
            return await _connection.QueryAsync<Inventory>(query);
        }

        public async Task AddAsync(Inventory inventory)
        {
            var query = "INSERT INTO Inventories (BookID, QuantityAvailable, QuantitySold, QuantityBorrowed) VALUES (@BookID, @QuantityAvailable, @QuantitySold, @QuantityBorrowed)";
            await _connection.ExecuteAsync(query, inventory);
        }

        public void Update(Inventory inventory)
        {
            var query = "UPDATE Inventories SET QuantityAvailable = @QuantityAvailable, QuantitySold = @QuantitySold, QuantityBorrowed = @QuantityBorrowed WHERE BookID = @BookID";
            _connection.Execute(query, inventory);
        }

        public void Delete(Inventory inventory)
        {
            var query = "DELETE FROM Inventories WHERE BookID = @BookID";
            _connection.Execute(query, new { BookID = inventory.Book.ID });
        }

        public async Task<Inventory> GetByNameAsync(string name)
        {
            var query = @"SELECT i.* 
                  FROM Inventories i
                  JOIN Books b ON i.BookID = b.ID
                  WHERE b.Name = @Name";
            return await _connection.QuerySingleOrDefaultAsync<Inventory>(query, new { Name = name });
        }

    }
}