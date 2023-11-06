using Application.DTO.Inventory;
using MediatR;

namespace Application.Commands.Inventory
{
    public class UpdateInventoryCommand : IRequest<Unit>
    {
        public InventoryUDto InventoryDto { get; set; }

        public UpdateInventoryCommand(InventoryUDto inventoryDto)
        {
            InventoryDto = inventoryDto;
        }
    }
}