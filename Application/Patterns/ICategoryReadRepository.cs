using Domain.Entities;

namespace Application.Patterns
{
	public interface ICategoryReadRepository
    {
		Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken = default);
		Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<Category> GetByNameAsync(string name, CancellationToken cancellationToken = default);
	}

}