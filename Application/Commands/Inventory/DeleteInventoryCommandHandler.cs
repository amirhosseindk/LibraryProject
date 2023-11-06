using Application.UseCases.Inventory;
using MediatR;

namespace Application.Commands.Inventory
{
    public class DeleteInventoryCommandHandler : IRequestHandler<DeleteInventoryCommand, Unit>
    {
        private readonly IInventoryService _inventoryService;

        public DeleteInventoryCommandHandler(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<Unit> Handle(DeleteInventoryCommand request, CancellationToken cancellationToken)
        {
            await _inventoryService.DeleteInventory(request.BookId);
            return Unit.Value;
        }
    }
}