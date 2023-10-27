using Application.DTO.Book;
using Application.DTO.Inventory;
using Application.Patterns;
using Application.UseCases.Inventory;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IRepository<Inventory> _inventoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InventoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _inventoryRepository = _unitOfWork.InventoryRepository;
        }

        public async Task UpdateInventory(InventoryUDto inventoryDto)
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
            await _unitOfWork.SaveAsync();
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
                Book = new BookRDto
                {
                    ID = inventory.Book.ID,
                    Name = inventory.Book.Name
                },
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
                Book = new BookRDto
                {
                    ID = inventory.Book.ID,
                    Name = inventory.Book.Name
                },
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
            await _unitOfWork.SaveAsync();
        }
    }
}