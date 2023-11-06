using Application.DTO.Author;
using MediatR;

namespace Application.Commands.Author
{
    public class UpdateAuthorCommand : IRequest<Unit>
    {
        public AuthorUDto AuthorDto { get; set; }
        public UpdateAuthorCommand(AuthorUDto authorDto)
        {
            AuthorDto = authorDto;
        }
    }
}