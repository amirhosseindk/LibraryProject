using Application.DTO.Book;
using MediatR;

namespace Application.Commands.Book
{
    public class UpdateBookCommand : IRequest<Unit>
    {
        public BookUDto BookDto { get; set; }
        public UpdateBookCommand(BookUDto bookDto)
        {
            BookDto = bookDto;
        }
    }
}