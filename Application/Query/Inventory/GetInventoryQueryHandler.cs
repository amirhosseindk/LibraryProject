using Application.DTO.Inventory;
using Application.UseCases.Inventory;
using MediatR;

namespace Application.Query.Inventory
{
    public class GetInventoryQueryHandler : IRequestHandler<GetInventoryQuery, InventoryRDto>
    {
        private readonly IInventoryService _inventoryService;

        public GetInventoryQueryHandler(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<InventoryRDto> Handle(GetInventoryQuery request, CancellationToken cancellationToken)
        {
            return await _inventoryService.GetInventoryDetailsByBookId(request.BookId);
        }
    }
}