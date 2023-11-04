﻿using Application.DTO.Book;
using Application.Patterns;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private IDbConnection _connection;

        public BookRepository(IDbConnection dbConnection)
        {
            _connection = dbConnection;
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            var query = @"
                SELECT b.*, 
                       a.FirstName + ' ' + a.LastName as AuthorName, 
                       c.Name as CategoryName
                FROM Books b
                LEFT JOIN Authors a ON b.AuthorID = a.ID
                LEFT JOIN Categories c ON b.CategoryID = c.ID
                WHERE b.ID = @Id";
            var result = await _connection.QuerySingleOrDefaultAsync<BookRDto>(query, new { Id = id });
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

        public async Task<Book> GetByNameAsync(string name)
        {
            var query = @"
                SELECT b.*, 
                       a.FirstName + ' ' + a.LastName as AuthorName, 
                       c.Name as CategoryName
                FROM Books b
                LEFT JOIN Authors a ON b.AuthorID = a.ID
                LEFT JOIN Categories c ON b.CategoryID = c.ID
                WHERE b.Name = @Name";
            var result = await _connection.QuerySingleOrDefaultAsync<BookRDto>(query, new { Name = name });

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

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var query = @"
                SELECT b.*, 
                       a.FirstName + ' ' + a.LastName as AuthorName, 
                       c.Name as CategoryName
                FROM Books b
                LEFT JOIN Authors a ON b.AuthorID = a.ID
                LEFT JOIN Categories c ON b.CategoryID = c.ID";
            var results = await _connection.QueryAsync<BookRDto>(query);
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

        public async Task AddAsync(Book book)
        {
            var query = "INSERT INTO Books (Name, AuthorID, CategoryID, Publisher, Price, Score, Summary, PublishYear) VALUES (@Name, @AuthorID, @CategoryID, @Publisher, @Price, @Score, @Summary, @PublishYear)";
            var parameters = new
            {
                Name = book.Name,
                AuthorID = book.Author.ID,
                CategoryID = book.Category.ID,
                Publisher = book.Publisher,
                Price = book.Price,
                Score = book.Score,
                Summary = book.Summary,
                PublishYear = book.PublishYear
            };
            await _connection.ExecuteAsync(query);
        }

        public async Task Update(Book book)
        {
            var query = "UPDATE Books SET Name = @Name, AuthorID = @AuthorID, CategoryID = @CategoryID, Publisher = @Publisher, Price = @Price, Score = @Score, Summary = @Summary, PublishYear = @PublishYear WHERE ID = @ID";
            var parameters = new
            {
                ID = book.ID,
                Name = book.Name,
                AuthorID = book.Author.ID,
                CategoryID = book.Category.ID,
                Publisher = book.Publisher,
                Price = book.Price,
                Summary = book.Summary,
                Score = book.Score,
                PublishYear = book.PublishYear
            };
            _connection.Execute(query);
        }

        public async Task Delete(Book book)
        {
            var query = "DELETE FROM Books WHERE ID = @ID";
            _connection.Execute(query, new { book.ID });
        }
    }
}