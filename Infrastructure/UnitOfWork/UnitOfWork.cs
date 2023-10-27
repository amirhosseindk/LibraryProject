using Application.Patterns;
using Domain.Entities;
using Infrastructure.Database;
using Infrastructure.Repositories;
using System.Data;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        public IDbConnection _connection;
        public IDbTransaction _transaction;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
            _connection = _dbContext.CreateConnection();
            _transaction = _connection.BeginTransaction();
        }

        public IDbConnection Connection => _connection;
        public IDbTransaction Transaction => _transaction;

        public IRepository<User> UserRepository => new UserRepository(this);
        public IRepository<Book> BookRepository => new BookRepository(this);
        public IRepository<Author> AuthorRepository => new AuthorRepository(this);
        public IRepository<Category> CategoryRepository => new CategoryRepository(this);
        public IRepository<Inventory> InventoryRepository => new InventoryRepository(this);

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