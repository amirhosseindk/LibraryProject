using Application.DTO.Book;
using MediatR;

namespace Application.Query.Book
{
    public class GetBooksQuery : IRequest<IEnumerable<BookRDto>>
    {
    }
}