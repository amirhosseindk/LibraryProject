using Application.UseCases.Author;
using MediatR;

namespace Application.Commands.Author
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Unit>
    {
        private readonly IAuthorService _authorService;

        public UpdateAuthorCommandHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            _authorService.UpdateAuthor(request.AuthorDto, cancellationToken);
            return Unit.Task;
        }
    }
}