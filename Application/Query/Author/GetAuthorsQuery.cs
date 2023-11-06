using Application.DTO.Author;
using MediatR;

namespace Application.Query.Author
{
    public class GetAuthorsQuery : IRequest<IEnumerable<AuthorRDto>>
    {
    }
}