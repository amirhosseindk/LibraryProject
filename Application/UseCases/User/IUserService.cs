using Application.DTO.User;
using Domain.Entities;

namespace Application.UseCases.User
{
    public interface IUserService
    {
        Task<UserRoles> GetUserRole(int userId);
        Task<int> RegisterUser(UserCDto userDto);
        Task UpdateUser(UserUDto userDto);
        Task DeleteUser(int userId);
        Task<UserRDto> GetUserDetails(int userId);
        Task<IEnumerable<UserRDto>> ListUsers();
    }
}