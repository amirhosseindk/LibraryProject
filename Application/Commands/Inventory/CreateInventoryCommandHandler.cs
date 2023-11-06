using Application.UseCases.Inventory;
using MediatR;

namespace Application.Commands.Inventory
{
    public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, int>
    {
        private readonly IInventoryService _inventoryService;

        public CreateInventoryCommandHandler(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<int> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
        {
            return await _inventoryService.CreateInventory(request.InventoryDto);
        }
    }
}