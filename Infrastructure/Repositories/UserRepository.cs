using Application.Patterns;
using Dapper;
using Domain.Entities;
using Infrastructure.Database;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbSession _session;

        public UserRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Users WHERE ID = @Id";
            return await _session.Connection.QuerySingleOrDefaultAsync<User>(query, new { Id = id }, _session.Transaction);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var query = "SELECT * FROM Users";
            return await _session.Connection.QueryAsync<User>(query, _session.Transaction);
        }

        public async Task AddAsync(User user)
        {
            var query = "INSERT INTO Users (FirstName, LastName, Email, Phone, Username, Password, Address, DateOfBirth, Role) VALUES (@FirstName, @LastName, @Email, @Phone, @Username, @Password, @Address, @DateOfBirth, @UserRole)";
            await _session.Connection.ExecuteAsync(query, user, _session.Transaction);
        }

        public void Update(User user)
        {
            var query = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone, Username = @Username, Password = @Password, Address = @Address, DateOfBirth = @DateOfBirth, Role = @UserRole WHERE ID = @ID";
            _session.Connection.Execute(query, user, _session.Transaction);
        }

        public void Delete(int ID)
        {
            var query = "DELETE FROM Users WHERE ID = @ID";
            _session.Connection.Execute(query,ID, _session.Transaction);
        }

        public async Task<User> GetByNameAsync(string username)
        {
            var query = "SELECT * FROM Users WHERE Username = @username";
            return await _session.Connection.QuerySingleOrDefaultAsync<User>(query, new { Username = username }, _session.Transaction);
        }
    }
}