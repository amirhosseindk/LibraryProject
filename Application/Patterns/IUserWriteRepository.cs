using Domain.Entities;

namespace Application.Patterns
{
	public interface IUserWriteRepository
    {
		Task AddAsync(User user, CancellationToken cancellationToken = default);
		void Update(User user, CancellationToken cancellationToken = default);
		void Delete(int id, CancellationToken cancellationToken = default);
	}
}