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
        public Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            _authorService.DeleteAuthor(request.Id);
            return Unit.Task;
        }
    }
}