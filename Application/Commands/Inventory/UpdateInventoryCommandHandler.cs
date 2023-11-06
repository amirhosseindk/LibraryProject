using Application.UseCases.Inventory;
using MediatR;

namespace Application.Commands.Inventory
{
    public class UpdateInventoryCommandHandler : IRequestHandler<UpdateInventoryCommand, Unit>
    {
        private readonly IInventoryService _inventoryService;

        public UpdateInventoryCommandHandler(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<Unit> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
        {
            await _inventoryService.UpdateInventory(request.InventoryDto);
            return Unit.Value;
        }
    }
}