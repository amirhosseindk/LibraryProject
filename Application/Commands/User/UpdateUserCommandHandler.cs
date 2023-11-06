using Application.UseCases.User;
using MediatR;

namespace Application.Commands.User
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUserService _userService;
        public UpdateUserCommandHandler(IUserService userService) 
        {
            _userService = userService;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await _userService.UpdateUser(request.UserDto);
            return Unit.Value;
        }
    }
}