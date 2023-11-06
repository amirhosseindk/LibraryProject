using Application.DTO.Inventory;
using MediatR;

namespace Application.Query.Inventory
{
    public class GetInventoriesQuery : IRequest<IEnumerable<InventoryRDto>>
    {
    }
}