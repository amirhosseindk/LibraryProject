using Application.DTO.User;
using MediatR;

namespace Application.Commands.User
{
    public class CreateUserCommand : IRequest<int>
    {
        public UserCDto UserDto { get; set; }

        public CreateUserCommand(UserCDto userDto)
        {
            UserDto = userDto;
        }
    }
}