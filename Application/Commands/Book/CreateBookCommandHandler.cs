using Application.UseCases.Book;
using MediatR;

namespace Application.Commands.Book
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly IBookService _bookService;

        public CreateBookCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            return await _bookService.CreateBook(request.BookDto, cancellationToken);
        }
    }
}