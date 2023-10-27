using Application.DTO.User;

namespace Application.UseCases.User
{
    public interface IUserService
    {
        Task RegisterUser(UserCDto userDto);
        Task UpdateUser(UserUDto userDto);
        Task DeleteUser(int userId);
        Task<UserRDto> GetUserDetails(int userId);
        Task<IEnumerable<UserRDto>> ListUsers();
        Task AssignUserRole(int userId, string roleDto);
    }
}