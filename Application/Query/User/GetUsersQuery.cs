using Application.DTO.User;
using MediatR;

namespace Application.Query.User
{
    public class GetUsersQuery : IRequest<IEnumerable<UserRDto>>
    {
    }
}