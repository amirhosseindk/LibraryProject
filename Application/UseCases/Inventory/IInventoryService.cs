using Application.DTO.Inventory;

namespace Application.UseCases.Inventory
{
    public interface IInventoryService
    {
        Task UpdateInventory(InventoryUDto inventoryDto);
        Task<InventoryRDto> GetInventoryDetailsByBookId(int bookId);
        Task<IEnumerable<InventoryRDto>> ListInventories();
    }
}