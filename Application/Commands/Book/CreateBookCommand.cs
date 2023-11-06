using Application.DTO.Book;
using MediatR;

namespace Application.Commands.Book
{
    public class CreateBookCommand : IRequest<int>
    {
        public BookCDto BookDto { get; set; }

        public CreateBookCommand(BookCDto bookDto)
        {
            BookDto = bookDto;
        }
    }
}