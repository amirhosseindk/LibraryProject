using Application.DTO.Book;
using Application.Patterns;
using Dapper;
using Domain.Entities;
using Infrastructure.Database;
using System.Data;

namespace Infrastructure.Repositories
{
    public class BookReadRepository : IBookReadRepository
    {
        private readonly DbSession _session;
        public BookReadRepository( DbSession session )
        {
            _session = session;
        }
        public async Task<Book> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var query = @"
                SELECT b.*, 
                       a.FirstName + ' ' + a.LastName as AuthorName, 
                       c.Name as CategoryName
                FROM Books b
                LEFT JOIN Authors a ON b.AuthorID = a.ID
                LEFT JOIN Categories c ON b.CategoryID = c.ID
                WHERE b.ID = @Id";

            var result = await _session.Connection.QueryFirstOrDefaultAsync<BookRDto>(
                new CommandDefinition(
                    query,
                    new { Id = id },
                    _session.Transaction,
                    cancellationToken: cancellationToken
                    )
                ).ConfigureAwait(false);

            var book = new Book
            {
                ID = result.ID,
                Name = result.Name,
                Author = new Author { FirstName = result.AuthorName.Split(' ')[0], LastName = result.AuthorName.Split(' ')[1] },
                Category = new Category { Name = result.CategoryName },
                Publisher = result.Publisher,
                Price = result.Price,
                Score = result.Score,
                Summary = result.Summary,
                PublishYear = result.PublishYear
            };
            return book;
        }

        public async Task<Book> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var query = @"
                SELECT b.*, 
                       a.FirstName + ' ' + a.LastName as AuthorName, 
                       c.Name as CategoryName
                FROM Books b
                LEFT JOIN Authors a ON b.AuthorID = a.ID
                LEFT JOIN Categories c ON b.CategoryID = c.ID
                WHERE b.Name = @Name";

            var result = await _session.Connection.QueryFirstOrDefaultAsync<BookRDto>(
                new CommandDefinition(
                    query,
                    new { Name = name },
                    _session.Transaction,
                    cancellationToken: cancellationToken
                    )
                ).ConfigureAwait(false);

            if (result == null)
            {
                return null;
            }

            var book = new Book
            {
                ID = result.ID,
                Name = result.Name,
                Publisher = result.Publisher,
                Price = result.Price,
                Score = result.Score,
                Summary = result.Summary,
                PublishYear = result.PublishYear
            };

            if (!string.IsNullOrEmpty(result.AuthorName) && result.AuthorName.Contains(' '))
            {
                var names = result.AuthorName.Split(' ');
                book.Author = new Author { FirstName = names[0], LastName = names[1] };
            }

            if (!string.IsNullOrEmpty(result.CategoryName))
            {
                book.Category = new Category { Name = result.CategoryName };
            }

            return book;
        }

        public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var query = @"
                SELECT b.*, 
                       a.FirstName + ' ' + a.LastName as AuthorName, 
                       c.Name as CategoryName
                FROM Books b
                LEFT JOIN Authors a ON b.AuthorID = a.ID
                LEFT JOIN Categories c ON b.CategoryID = c.ID";

            var results = await _session.Connection.QueryAsync<BookRDto>(
                new CommandDefinition(
                    query,
                    _session.Transaction,
                    cancellationToken: cancellationToken
                    )
                ).ConfigureAwait(false);

            var books = results.Select(result => new Book
            {
                ID = result.ID,
                Name = result.Name,
                Author = new Author { FirstName = result.AuthorName.Split(' ')[0], LastName = result.AuthorName.Split(' ')[1] },
                Category = new Category { Name = result.CategoryName },
                Publisher = result.Publisher,
                Price = result.Price,
                Score = result.Score,
                Summary = result.Summary,
                PublishYear = result.PublishYear
            });
            return books;
        }
    }
}