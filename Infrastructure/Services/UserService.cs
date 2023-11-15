using Application.DTO.User;
using Application.Patterns;
using Application.UseCases.User;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork, IUserWriteRepository userWriteRepository, IUserReadRepository userReadRepository)
        {
            _unitOfWork = unitOfWork;
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
        }

        public async Task DeleteUser(int userId)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var existingUser = await _userReadRepository.GetByIdAsync(userId);
                if (existingUser == null)
                {
                    throw new Exception("User not found.");
                }

                _userWriteRepository.Delete(existingUser.ID);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<UserRDto> GetUserDetails(int userId)
        {
            var existingUser = await _userReadRepository.GetByIdAsync(userId);
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
            var Users = await _userReadRepository.GetAllAsync();
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
            _unitOfWork.BeginTransaction();
            try
            {
                var existingUser = await _userReadRepository.GetByNameAsync(userDto.Username);
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

                await _userWriteRepository.AddAsync(user);
                _unitOfWork.Commit();

                return user.ID;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task UpdateUser(UserUDto userDto)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var existingUser = await _userReadRepository.GetByIdAsync(userDto.ID);

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

                _userWriteRepository.Update(existingUser);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<UserRoles> GetUserRole(int userId)
        {
            var existingUser = await _userReadRepository.GetByIdAsync(userId);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }
            return existingUser.Role;
        }
    }
}