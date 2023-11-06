using Application.UseCases.Author;
using MediatR;

namespace Application.Commands.Author
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, int>
    {
        private readonly IAuthorService _authorService;

        public CreateAuthorCommandHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var res = await _authorService.CreateAuthor(request.AuthorDto);
            return res;
        }
    }
}