using Application.DTO.User;
using MediatR;

namespace Application.Commands.User
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public UserUDto UserDto { get; set; }
        public UpdateUserCommand(UserUDto userDto) 
        {
            UserDto = userDto;
        }
    }
}