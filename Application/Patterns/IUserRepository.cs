using Domain.Entities;

namespace Application.Patterns
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        void Update(User user);
        void Delete(int id);
        Task<User> GetByNameAsync(string username);
    }
}