using Domain.Entities;

namespace Application.Patterns
{
	public interface ICategoryWriteRepository
    {
		Task AddAsync(Category category, CancellationToken cancellationToken = default);
		Task UpdateAsync(Category category, CancellationToken cancellationToken = default);
		Task DeleteAsync(Category category, CancellationToken cancellationToken = default);
	}

}