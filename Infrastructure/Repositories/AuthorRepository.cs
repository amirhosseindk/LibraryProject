using Application.Patterns;
using Dapper;
using Domain.Entities;
using Infrastructure.Database;

namespace Infrastructure.Repositories
{
	public class AuthorRepository : IAuthorRepository
	{
		private readonly DbSession _session;

		public AuthorRepository(DbSession session)
		{
			_session = session;
		}

		public async Task<Author> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			var query = "SELECT * FROM Authors WHERE ID = @Id";
			var queryResult = await _session.Connection
				.QueryFirstOrDefaultAsync<Author>(
				new CommandDefinition(
					query,
					new { Id = id },
					_session.Transaction,
					cancellationToken: cancellationToken
				   )
				).ConfigureAwait(false);

			return queryResult;
		}

		public async Task<Author> GetByNameAsync(string lastName, CancellationToken cancellationToken = default)
		{
			var query = "SELECT * FROM Authors WHERE LastName = @LastName";
			var queryResult = await _session.Connection.QueryFirstOrDefaultAsync<Author>(
				new CommandDefinition(
					query,
					new { LastName = lastName },
					_session.Transaction,
					cancellationToken: cancellationToken
					)
				).ConfigureAwait(false);
			return queryResult;
		}

		public async Task<IEnumerable<Author>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var query = "SELECT * FROM Authors";
			return await _session.Connection.QueryAsync<Author>(
				new CommandDefinition(
					query,
					_session.Transaction,
					cancellationToken: cancellationToken
					)
				).ConfigureAwait(false);
		}

		public async Task AddAsync(Author author, CancellationToken cancellationToken = default)
		{
			var query = "INSERT INTO Authors (FirstName, LastName) VALUES (@FirstName, @LastName)";
			await _session.Connection.ExecuteAsync(
				new CommandDefinition(
					query, 
					author, 
					_session.Transaction,
					cancellationToken:cancellationToken
					)
				).ConfigureAwait(false);
		}

		public async Task Update(Author author, CancellationToken cancellationToken = default)
		{
			var query = "UPDATE Authors SET FirstName = @FirstName, LastName = @LastName WHERE ID = @ID";
			await _session.Connection.ExecuteAsync(
				new CommandDefinition(
					query,
					author,
					_session.Transaction,
					cancellationToken: cancellationToken
					)
				).ConfigureAwait(false);
		}

		public async Task Delete(Author author, CancellationToken cancellationToken = default)
		{
			var query = "DELETE FROM Authors WHERE ID = @ID";
			await _session.Connection.ExecuteAsync(
				new CommandDefinition(
					query,
					new { author.ID },
					_session.Transaction,
					cancellationToken: cancellationToken
					)
				).ConfigureAwait(false);
		}
	}
}