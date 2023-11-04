using Application.Patterns;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IDbConnection _connection;

        public UserRepository(IDbConnection connection)
        {
            _connection = connection;
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
            var query = "INSERT INTO Users (FirstName, LastName, Email, Phone, Username, Password, Address, DateOfBirth, Role) VALUES (@FirstName, @LastName, @Email, @Phone, @Username, @Password, @Address, @DateOfBirth, @UserRole)";
            await _connection.ExecuteAsync(query, user);
        }

        public void Update(User user)
        {
            var query = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone, Username = @Username, Password = @Password, Address = @Address, DateOfBirth = @DateOfBirth, Role = @UserRole WHERE ID = @ID";
            _connection.Execute(query, user);
        }

        public void Delete(int ID)
        {
            var query = "DELETE FROM Users WHERE ID = @ID";
            _connection.Execute(query,ID);
        }

        public async Task<User> GetByNameAsync(string username)
        {
            var query = "SELECT * FROM Users WHERE Username = @username";
            return await _connection.QuerySingleOrDefaultAsync<User>(query, new { Username = username });
        }
    }
}