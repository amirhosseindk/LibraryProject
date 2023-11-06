using Application.DTO.Author;
using MediatR;

namespace Application.Commands.Author
{
    public class CreateAuthorCommand : IRequest<int>
    {
        public AuthorCDto AuthorDto { get; set; }

        public CreateAuthorCommand(AuthorCDto authorDto)
        {
            AuthorDto = authorDto;
        }
    }
}