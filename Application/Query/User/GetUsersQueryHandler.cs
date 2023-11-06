using Application.DTO.User;
using Application.UseCases.User;
using MediatR;

namespace Application.Query.User
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserRDto>>
    {
        private readonly IUserService _userService;
        public GetUsersQueryHandler(IUserService userService)
        {
            _userService = userService; 
        }
        public async Task<IEnumerable<UserRDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userService.ListUsers();
        }
    }
}