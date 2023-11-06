using Application.DTO.Book;
using Application.UseCases.Book;
using MediatR;

namespace Application.Query.Book
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<BookRDto>>
    {
        private readonly IBookService _bookService;
        public GetBooksQueryHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IEnumerable<BookRDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            return await _bookService.ListBooks(cancellationToken);
        }
    }
}