using Application.DTO.User;
using Domain.Entities;

namespace Application.UseCases.User
{
    public interface IUserService
    {
        Task RegisterUser(UserCDto userDto);
        Task UpdateUser(UserUDto userDto);
        Task DeleteUser(int userId);
        Task<UserRDto> GetUserDetails(int userId);
        Task<IEnumerable<UserRDto>> ListUsers();
        Task AssignUserRole(int userId, UserRole role);
    }
}