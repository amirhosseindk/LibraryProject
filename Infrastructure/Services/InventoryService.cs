using Application.DTO.Inventory;
using Application.Patterns;
using Application.UseCases.Inventory;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InventoryService(IUnitOfWork unitOfWork, IInventoryRepository inventoryRepository)
        {
            _unitOfWork = unitOfWork;
            _inventoryRepository = inventoryRepository;
        }

        public async Task UpdateInventory(InventoryUDto inventoryDto)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var inventory = await _inventoryRepository.GetByIdAsync(inventoryDto.BookId);
                if (inventory == null)
                {
                    throw new Exception("Inventory not found.");
                }

                inventory.QuantityAvailable = inventoryDto.QuantityAvailable;
                inventory.QuantitySold = inventoryDto.QuantitySold;
                inventory.QuantityBorrowed = inventoryDto.QuantityBorrowed;

                _inventoryRepository.Update(inventory);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }

        }

        public async Task<InventoryRDto> GetInventoryDetailsByBookId(int bookId)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(bookId);
            if (inventory == null)
            {
                throw new Exception("Inventory not found.");
            }

            return new InventoryRDto
            {
                BookId = bookId,
                QuantityAvailable = inventory.QuantityAvailable,
                QuantitySold = inventory.QuantitySold,
                QuantityBorrowed = inventory.QuantityBorrowed
            };
        }

        public async Task<IEnumerable<InventoryRDto>> ListInventories()
        {
            var inventories = await _inventoryRepository.GetAllAsync();
            return new List<InventoryRDto>(inventories.Select(inventory => new InventoryRDto
            {
                BookId = inventory.BookId,
                QuantityAvailable = inventory.QuantityAvailable,
                QuantitySold = inventory.QuantitySold,
                QuantityBorrowed = inventory.QuantityBorrowed
            }));
        }

        public async Task IncreaseQuantityAvailable(int bookId) => await UpdateQuantity(bookId, "available", true);
        public async Task IncreaseQuantitySold(int bookId) => await UpdateQuantity(bookId, "sold", true);
        public async Task IncreaseQuantityBorrowed(int bookId) => await UpdateQuantity(bookId, "borrowed", true);
        public async Task DecreaseQuantityAvailable(int bookId) => await UpdateQuantity(bookId, "available", false);
        public async Task DecreaseQuantitySold(int bookId) => await UpdateQuantity(bookId, "sold", false);
        public async Task DecreaseQuantityBorrowed(int bookId) => await UpdateQuantity(bookId, "borrowed", false);

        private async Task UpdateQuantity(int bookId, string type, bool increase)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var inventory = await _inventoryRepository.GetByIdAsync(bookId);
                if (inventory == null)
                {
                    throw new Exception("Inventory not found.");
                }

                switch (type)
                {
                    case "available":
                        inventory.QuantityAvailable += increase ? 1 : -1;
                        break;
                    case "sold":
                        inventory.QuantitySold += increase ? 1 : -1;
                        break;
                    case "borrowed":
                        inventory.QuantityBorrowed += increase ? 1 : -1;
                        break;
                }

                _inventoryRepository.Update(inventory);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<int> CreateInventory(InventoryCDto inventoryCDto)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var bookId = inventoryCDto.BookId;
                var existingInventory = await _inventoryRepository.GetByIdAsync(bookId);
                if (existingInventory != null)
                {
                    throw new Exception("Inventory for this book already exists.");
                }
                else
                {
                    var newInventory = new Inventory
                    {
                        BookId = bookId,
                        QuantityAvailable = inventoryCDto.QuantityAvailable,
                        QuantitySold = inventoryCDto.QuantitySold,
                        QuantityBorrowed = inventoryCDto.QuantityBorrowed
                    };
                    await _inventoryRepository.AddAsync(newInventory);
                }
                _unitOfWork.Commit();
                return bookId;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task DeleteInventory(int bookId)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var inventory = await _inventoryRepository.GetByIdAsync(bookId);
                if (inventory == null)
                {
                    throw new Exception("Inventory not found.");
                }

                _inventoryRepository.Delete(inventory);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    }
}