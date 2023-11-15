using Domain.Entities;

namespace Application.Patterns
{
	public interface IUserReadRepository
    {
		Task<User> GetByIdAsync(int id);
		Task<IEnumerable<User>> GetAllAsync();
		Task<User> GetByNameAsync(string username);
	}

}