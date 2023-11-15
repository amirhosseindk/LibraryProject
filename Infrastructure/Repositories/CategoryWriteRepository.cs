using Application.Patterns;
using Domain.Entities;
using Infrastructure.Database;

namespace Infrastructure.Repositories
{
	public class CategoryWriteRepository : ICategoryWriteRepository
	{
        private readonly AppDbContext _context;
        public CategoryWriteRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
		public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
		{
			_context.Categories.Add(category);
			await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		}

		public async Task UpdateAsync(Category category, CancellationToken cancellationToken = default)
		{
			_context.Categories.Update(category);
			await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		}

		public async Task DeleteAsync(Category category, CancellationToken cancellationToken = default)
		{
			_context.Categories.Remove(category);
			await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		}
	}
}