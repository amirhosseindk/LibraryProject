using Application.DTO.User;
using Application.UseCases.User;
using MediatR;

namespace Application.Query.User
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserRDto>
    {
        private readonly IUserService _userService;
        public GetUserQueryHandler(IUserService userService) 
        { 
            _userService = userService;
        }
        public async Task<UserRDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUserDetails(request.Id);
        }
    }
}