using Domain.Entities;
using System.Data;

namespace Application.Patterns
{
    public interface IUnitOfWork
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Book> BookRepository { get; }
        IRepository<Author> AuthorRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<Inventory> InventoryRepository { get; }
        Task SaveAsync();
        void Dispose();
    }
}
