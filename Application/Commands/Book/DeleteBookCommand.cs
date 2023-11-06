using MediatR;

namespace Application.Commands.Book
{
    public class DeleteBookCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public DeleteBookCommand(int id)
        {
            Id = id;
        }
    }
}