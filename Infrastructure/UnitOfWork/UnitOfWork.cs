using Application.Patterns;
using Domain.Entities;
using Infrastructure.Repositories;
using System.Data;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public UnitOfWork(IDbConnection connection)
        {
            _connection = connection;
            _transaction = _connection.BeginTransaction();
        }

        public IRepository<User> UserRepository => new UserRepository(_connection, _transaction);
        public IRepository<Book> BookRepository => new BookRepository(_connection, _transaction);
        public IRepository<Author> AuthorRepository => new AuthorRepository(_connection, _transaction);
        public IRepository<Category> CategoryRepository => new CategoryRepository(_connection, _transaction);
        public IRepository<Inventory> InventoryRepository => new InventoryRepository(_connection, _transaction);

        public async Task SaveAsync()
        {
            _transaction.Commit();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
}