using Application.Patterns;
using Domain.Entities;
using Infrastructure.Database;

namespace Infrastructure.Repositories
{
	public class AuthorWriteRepository : IAuthorWriteRepository
	{
		private readonly AppDbContext _context;

		public AuthorWriteRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Author author, CancellationToken cancellationToken = default)
		{
			_context.Authors.Add(author);
			await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		}

		public async Task Update(Author author, CancellationToken cancellationToken = default)
		{
			_context.Update(author);
			await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		}

		public async Task Delete(Author author, CancellationToken cancellationToken = default)
		{
			_context.Authors.Remove(author);
			await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		}
	}
}