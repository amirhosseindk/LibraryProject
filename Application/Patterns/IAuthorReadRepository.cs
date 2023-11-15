using Domain.Entities;

namespace Application.Patterns
{
	public interface IAuthorReadRepository
	{
		Task<Author> GetByIdAsync(int id, CancellationToken cancellationToken = default);
		Task<Author> GetByNameAsync(string lastName, CancellationToken cancellationToken = default);
		Task<IEnumerable<Author>> GetAllAsync(CancellationToken cancellationToken = default);
	}
}