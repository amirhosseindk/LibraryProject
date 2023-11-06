using Application.UseCases.Author;
using MediatR;

namespace Application.Commands.Author
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Unit>
    {
        private readonly IAuthorService _authorService;
        public DeleteAuthorCommandHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            await _authorService.DeleteAuthor(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}