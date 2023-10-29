using Application.DTO.User;
using Application.Patterns;
using Application.UseCases.User;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteUser(int userId)
        {
            var existingUser = await _userRepository.GetByIdAsync(userId);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            _userRepository.Delete(existingUser);
            await _unitOfWork.SaveAsync();
        }

        public async Task<UserRDto> GetUserDetails(int userId)
        {
            var existingUser = await _userRepository.GetByIdAsync(userId);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            return new UserRDto
            {
                ID = existingUser.ID,
                FullName = existingUser.FirstName + " " + existingUser.LastName,
                Email = existingUser.Email,
                Address = existingUser.Address,
                DateOfBirth = existingUser.DateOfBirth,
                Phone = existingUser.Phone,
                UserRole = existingUser.Role
            };
        }

        public async Task<IEnumerable<UserRDto>> ListUsers()
        {
            var Users = await _userRepository.GetAllAsync();
            return Users.Select(a => new UserRDto
            {
                ID = a.ID,
                FullName = a.FirstName + " " + a.LastName,
                Email = a.Email,
                Address = a.Address,
                DateOfBirth = a.DateOfBirth,
                Phone = a.Phone,
                UserRole = a.Role
            });
        }

        public async Task<int> RegisterUser(UserCDto userDto)
        {
            var existingUser = await _userRepository.GetByNameAsync(userDto.Username);
            if (existingUser != null)
            {
                throw new Exception("User with this Username already exists.");
            }

            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Username = userDto.Username,
                Password = userDto.Password,
                Email = userDto.Email,
                Address = userDto.Address,
                DateOfBirth = userDto.DateOfBirth,
                Phone = userDto.Phone,
                Role = userDto.UserRole
            };

            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();

            return user.ID;
        }

        public async Task UpdateUser(UserUDto userDto)
        {
            var existingUser = await _userRepository.GetByIdAsync(userDto.ID);

            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            existingUser.FirstName = userDto.FirstName;
            existingUser.LastName = userDto.LastName;
            existingUser.Username = userDto.Username;
            existingUser.Password = userDto.Password;
            existingUser.Email = userDto.Email;
            existingUser.Address = userDto.Address;
            existingUser.DateOfBirth = userDto.DateOfBirth;
            existingUser.Phone = userDto.Phone;
            existingUser.Role = userDto.UserRole;

            _userRepository.Update(existingUser);
            await _unitOfWork.SaveAsync();
        }

        public async Task<UserRoles> GetUserRole(int userId)
        {
            var existingUser = await _userRepository.GetByIdAsync(userId);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }
            return existingUser.Role;
        }
    }
}