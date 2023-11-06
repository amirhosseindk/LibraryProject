using Application.UseCases.Book;
using MediatR;

namespace Application.Commands.Book
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand,Unit>
    {
        private readonly IBookService _bookService;

        public UpdateBookCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            _bookService.UpdateBook(request.BookDto);
            return Unit.Task;
        }
    }
}