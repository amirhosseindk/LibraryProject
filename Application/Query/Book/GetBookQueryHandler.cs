using Application.DTO.Book;
using Application.UseCases.Book;
using MediatR;

namespace Application.Query.Book
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BookRDto>
    {
        private readonly IBookService _bookService;

        public GetBookQueryHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<BookRDto> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            if (request.ID != 0 && request.Name == null)
            {
                return await _bookService.GetBookDetails(request.ID, cancellationToken);
            }
            else if (request.Name != null && request.ID == 0)
            {
                return await _bookService.GetBookDetails(request.Name, cancellationToken);
            }
            else
            {
                throw new Exception("Invalid query");
            }
        }
    }
}