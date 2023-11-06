using MediatR;

namespace Application.Commands.Inventory
{
    public class DeleteInventoryCommand : IRequest<Unit>
    {
        public int BookId { get; set; }

        public DeleteInventoryCommand(int bookId)
        {
            BookId = bookId;
        }
    }
}