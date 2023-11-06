using Application.DTO.Inventory;
using MediatR;

namespace Application.Commands.Inventory
{
    public class CreateInventoryCommand : IRequest<int>
    {
        public InventoryCDto InventoryDto { get; set; }

        public CreateInventoryCommand(InventoryCDto inventoryDto)
        {
            InventoryDto = inventoryDto;
        }
    }
}