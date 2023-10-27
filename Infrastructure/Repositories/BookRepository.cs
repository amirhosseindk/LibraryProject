using Application.Patterns;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public BookRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Books WHERE ID = @Id";
            return await _connection.QuerySingleOrDefaultAsync<Book>(query, new { Id = id });
        }

        public async Task<Book> GetByNameAsync(string name)
        {
            var query = "SELECT * FROM Books WHERE Name = @Name";
            return await _connection.QuerySingleOrDefaultAsync<Book>(query, new { Name = name });
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var query = "SELECT * FROM Books";
            return await _connection.QueryAsync<Book>(query);
        }

        public async Task AddAsync(Book book)
        {
            var query = "INSERT INTO Books (Name, AuthorID, CategoryID, Publisher, Price, Score, Summary, PublishYear) VALUES (@Name, @AuthorID, @CategoryID, @Publisher, @Price, @Score, @Summary, @PublishYear)";
            await _connection.ExecuteAsync(query, book);
        }

        public void Update(Book book)
        {
            var query = "UPDATE Books SET Name = @Name, AuthorID = @AuthorID, CategoryID = @CategoryID, Publisher = @Publisher, Price = @Price, Score = @Score, Summary = @Summary, PublishYear = @PublishYear WHERE ID = @ID";
            _connection.Execute(query, book);
        }

        public void Delete(Book book)
        {
            var query = "DELETE FROM Books WHERE ID = @ID";
            _connection.Execute(query, new { book.ID });
        }
    }
}