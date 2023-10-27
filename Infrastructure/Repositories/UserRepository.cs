using Application.Patterns;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public UserRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Users WHERE ID = @Id";
            return await _connection.QuerySingleOrDefaultAsync<User>(query, new { Id = id });
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var query = "SELECT * FROM Users";
            return await _connection.QueryAsync<User>(query);
        }

        public async Task AddAsync(User user)
        {
            var query = "INSERT INTO Users (FirstName, LastName, Email, Phone, Username, Password, Address, DateOfBirth) VALUES (@FirstName, @LastName, @Email, @Phone, @Username, @Password, @Address, @DateOfBirth)";
            await _connection.ExecuteAsync(query, user);
        }

        public void Update(User user)
        {
            var query = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone, Username = @Username, Password = @Password, Address = @Address, DateOfBirth = @DateOfBirth WHERE ID = @ID";
            _connection.Execute(query, user);
        }

        public void Delete(User user)
        {
            var query = "DELETE FROM Users WHERE ID = @ID";
            _connection.Execute(query, new { user.ID });
        }

        public async Task<User> GetByNameAsync(string name)
        {
            var query = "SELECT * FROM Users WHERE Name = @name";
            return await _connection.QuerySingleOrDefaultAsync<User>(query, new { Name = name });
        }
    }
}