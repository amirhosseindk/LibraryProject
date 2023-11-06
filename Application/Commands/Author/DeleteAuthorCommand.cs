using MediatR;

namespace Application.Commands.Author
{
    public class DeleteAuthorCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public DeleteAuthorCommand(int id)
        {
            Id = id;
        }
    }
}