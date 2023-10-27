using Application.Patterns;
using Infrastructure.Repositories;
using System.Data;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;

        public BookRepository Books { get; private set; }
        public AuthorRepository Authors { get; private set; }
        public CategoryRepository Categories { get; private set; }
        public UserRepository Users { get; private set; }
        public InventoryRepository Inventories { get; private set; }

        public UnitOfWork(IDbConnection connection)
        {
            _connection = connection;
            _connection.Open();
            _transaction = _connection.BeginTransaction();

            Books = new BookRepository(_connection, _transaction);
            Authors = new AuthorRepository(_connection, _transaction);
            Categories = new CategoryRepository(_connection, _transaction);
            Users = new UserRepository(_connection, _transaction);
            Inventories = new InventoryRepository(_connection, _transaction);
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        public void Rollback()
        {
            try
            {
                _transaction.Rollback();
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        private void ResetRepositories()
        {
            Books = new BookRepository(_connection, _transaction);
            Authors = new AuthorRepository(_connection, _transaction);
            Categories = new CategoryRepository(_connection, _transaction);
            Users = new UserRepository(_connection, _transaction);
            Inventories = new InventoryRepository(_connection, _transaction);
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
}
