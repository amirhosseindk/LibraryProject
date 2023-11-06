using Application.UseCases.Book;
using MediatR;

namespace Application.Commands.Book
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Unit>
    {
        private readonly IBookService _bookService;
        public DeleteBookCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }
        public Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            _bookService.DeleteBook(request.Id,cancellationToken);
            return Unit.Task;
        }
    }
}