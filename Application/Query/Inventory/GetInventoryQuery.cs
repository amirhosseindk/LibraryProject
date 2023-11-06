using Application.DTO.Inventory;
using MediatR;

namespace Application.Query.Inventory
{
    public class GetInventoryQuery : IRequest<InventoryRDto>
    {
        public int BookId { get; set; }

        public GetInventoryQuery(int bookId)
        {
            BookId = bookId;
        }
    }
}