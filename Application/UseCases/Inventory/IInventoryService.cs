using Application.DTO.Inventory;

namespace Application.UseCases.Inventory
{
    public interface IInventoryService
    {
        Task UpdateInventory(InventoryUDto inventoryDto);
        Task<InventoryRDto> GetInventoryDetails(int inventoryId);
        Task<IEnumerable<InventoryRDto>> ListInventories();
    }
}