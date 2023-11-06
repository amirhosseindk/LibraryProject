using Application.DTO.Inventory;
using Application.UseCases.Inventory;
using MediatR;

namespace Application.Query.Inventory
{
    public class GetInventoriesQueryHandler : IRequestHandler<GetInventoriesQuery, IEnumerable<InventoryRDto>>
    {
        private readonly IInventoryService _inventoryService;

        public GetInventoriesQueryHandler(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<IEnumerable<InventoryRDto>> Handle(GetInventoriesQuery request, CancellationToken cancellationToken)
        {
            return await _inventoryService.ListInventories();
        }
    }
}