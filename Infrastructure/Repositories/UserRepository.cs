using Application.Patterns;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IDbConnection _connection => _unitOfWork.Connection;
        private IDbTransaction _transaction => _unitOfWork.Transaction;

        public UserRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Users WHERE ID = @Id";
            return await _connection.QuerySingleOrDefaultAsync<User>(query, new { Id = id }, _transaction);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var query = "SELECT * FROM Users";
            return await _connection.QueryAsync<User>(query, transaction: _transaction);
        }

        public async Task AddAsync(User user)
        {
            var query = "INSERT INTO Users (FirstName, LastName, Email, Phone, Username, Password, Address, DateOfBirth, Role) VALUES (@FirstName, @LastName, @Email, @Phone, @Username, @Password, @Address, @DateOfBirth, @UserRole)";
            await _connection.ExecuteAsync(query, user, _transaction);
        }

        public void Update(User user)
        {
            var query = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone, Username = @Username, Password = @Password, Address = @Address, DateOfBirth = @DateOfBirth, Role = @UserRole WHERE ID = @ID";
            _connection.Execute(query, user, _transaction);
        }

        public void Delete(User user)
        {
            var query = "DELETE FROM Users WHERE ID = @ID";
            _connection.Execute(query, new { user.ID }, _transaction);
        }

        public async Task<User> GetByNameAsync(string username)
        {
            var query = "SELECT * FROM Users WHERE Username = @username";
            return await _connection.QuerySingleOrDefaultAsync<User>(query, new { Username = username }, _transaction);
        }
    }
}