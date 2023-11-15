using Domain.Entities;

namespace Application.Patterns
{
	public interface IAuthorWriteRepository
	{
		Task AddAsync(Author author, CancellationToken cancellationToken = default);
		Task Update(Author author, CancellationToken cancellationToken = default);
		Task Delete(Author author, CancellationToken cancellationToken = default);
	}
}