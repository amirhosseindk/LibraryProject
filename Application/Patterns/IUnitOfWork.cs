using Domain.Entities;

namespace Application.Patterns
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        IRepository<Book> BookRepository { get; }
        IRepository<Author> AuthorRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<Inventory> InventoryRepository { get; }
        Task SaveAsync();
        void Dispose();
    }
}
