﻿using Application.Patterns;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Repositories
{
    public class InventoryRepository : IRepository<Inventory>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IDbConnection _connection => _unitOfWork.Connection;
        private IDbTransaction _transaction => _unitOfWork.Transaction;

        public InventoryRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Inventory> GetByIdAsync(int bookId)
        {
            var query = "SELECT * FROM Inventories WHERE BookID = @BookID";
            return await _connection.QuerySingleOrDefaultAsync<Inventory>(query, new { BookID = bookId }, _transaction);
        }

        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            var query = "SELECT * FROM Inventories";
            return await _connection.QueryAsync<Inventory>(query, transaction: _transaction);
        }

        public async Task AddAsync(Inventory inventory)
        {
            var query = "INSERT INTO Inventories (BookID, QuantityAvailable, QuantitySold, QuantityBorrowed) VALUES (@BookID, @QuantityAvailable, @QuantitySold, @QuantityBorrowed)";
            await _connection.ExecuteAsync(query, inventory, _transaction);
        }

        public void Update(Inventory inventory)
        {
            var query = "UPDATE Inventories SET QuantityAvailable = @QuantityAvailable, QuantitySold = @QuantitySold, QuantityBorrowed = @QuantityBorrowed WHERE BookID = @BookID";
            _connection.Execute(query, inventory, _transaction);
        }

        public void Delete(Inventory inventory)
        {
            var query = "DELETE FROM Inventories WHERE BookID = @BookID";
            _connection.Execute(query, new { BookID = inventory.BookId }, _transaction);
        }

        public async Task<Inventory> GetByNameAsync(string name)
        {
            var query = @"SELECT i.* 
                  FROM Inventories i
                  JOIN Books b ON i.BookID = b.ID
                  WHERE b.Name = @Name";
            return await _connection.QuerySingleOrDefaultAsync<Inventory>(query, new { Name = name }, _transaction);
        }

    }
}