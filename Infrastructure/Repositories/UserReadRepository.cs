using Application.Patterns;
using Dapper;
using Domain.Entities;
using Infrastructure.Database;

namespace Infrastructure.Repositories
{
	public class UserReadRepository : IUserReadRepository
	{
		private readonly AppDbSession _session;

        public UserReadRepository(AppDbSession appDbSeasion)
        {
            _session = appDbSeasion; 
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

		public async Task<User> GetByNameAsync(string username)
		{
			var query = "SELECT * FROM Users WHERE Username = @username";
			return await _session.Connection.QuerySingleOrDefaultAsync<User>(query, new { Username = username }, _session.Transaction);
		}

	}
}