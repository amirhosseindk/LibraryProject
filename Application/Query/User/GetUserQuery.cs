using Application.DTO.User;
using MediatR;

namespace Application.Query.User
{
    public class GetUserQuery : IRequest<UserRDto>
    {
        public int Id { get; set; }
        public GetUserQuery(int id) 
        { 
            Id = id;
        }
    }
}