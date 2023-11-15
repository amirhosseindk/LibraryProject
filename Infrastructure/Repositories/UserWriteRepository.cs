using Application.Patterns;
using Domain.Entities;
using Infrastructure.Database;

namespace Infrastructure.Repositories
{
	public class UserWriteRepository : IUserWriteRepository
	{
		private readonly AppDbContext _context;

		public UserWriteRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(User user, CancellationToken cancellationToken = default)
		{
			_context.Users.Add(user);
			await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		}

		public async void Delete(int id, CancellationToken cancellationToken = default)
		{
			var user = await _context.Users.FindAsync(id);
			_context.Users.Remove(user);
			await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		}

		public async void Update(User user, CancellationToken cancellationToken = default)
		{
			_context.Users.Update(user);
			await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		}
	}
}