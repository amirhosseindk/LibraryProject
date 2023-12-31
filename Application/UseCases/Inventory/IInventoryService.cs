﻿using Application.DTO.Inventory;

namespace Application.UseCases.Inventory
{
    public interface IInventoryService
    {
        Task<int> CreateInventory(InventoryCDto inventoryCDto);
        Task UpdateInventory(InventoryUDto inventoryDto);
        Task<InventoryRDto> GetInventoryDetailsByBookId(int bookId);
        Task<IEnumerable<InventoryRDto>> ListInventories();
        Task DeleteInventory(int bookId);
        Task IncreaseQuantityAvailable(int bookId);
        Task IncreaseQuantitySold(int bookId);
        Task IncreaseQuantityBorrowed(int bookId);
        Task DecreaseQuantityAvailable(int bookId);
        Task DecreaseQuantitySold(int bookId);
        Task DecreaseQuantityBorrowed(int bookId);
    }
}