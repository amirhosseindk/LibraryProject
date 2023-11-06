using Application.Commands.Inventory;
using Application.UseCases.User;
using MediatR;

namespace Application.Commands.User
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserService _userService;
        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.RegisterUser(request.UserDto);
        }
    }
}