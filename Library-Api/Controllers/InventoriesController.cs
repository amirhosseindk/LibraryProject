using Application.DTO.Inventory;
using Application.UseCases.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace Library_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoriesController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inventories = await _inventoryService.ListInventories();
            return Ok(inventories);
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetByBookId(int bookId)
        {
            var inventory = await _inventoryService.GetInventoryDetailsByBookId(bookId);
            if (inventory == null)
                return NotFound();

            return Ok(inventory);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InventoryCDto inventoryCDto)
        {
            int newInventoryId = await _inventoryService.CreateInventory(inventoryCDto);
            return CreatedAtAction("GetByBookId", new { bookId = newInventoryId }, inventoryCDto);
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> Update(int bookId, [FromBody] InventoryUDto inventoryUDto)
        {
            if (bookId != inventoryUDto.BookId)
                return BadRequest();

            await _inventoryService.UpdateInventory(inventoryUDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _inventoryService.DeleteInventory(id);
            return NoContent();
        }

        [HttpPost("{bookId}/increase/available")]
        public async Task<IActionResult> IncreaseAvailable(int bookId)
        {
            await _inventoryService.IncreaseQuantityAvailable(bookId);
            return NoContent();
        }

        [HttpPost("{bookId}/increase/sold")]
        public async Task<IActionResult> IncreaseSold(int bookId)
        {
            await _inventoryService.IncreaseQuantitySold(bookId);
            return NoContent();
        }

        [HttpPost("{bookId}/increase/borrowed")]
        public async Task<IActionResult> IncreaseBorrowed(int bookId)
        {
            await _inventoryService.IncreaseQuantityBorrowed(bookId);
            return NoContent();
        }

        [HttpPost("{bookId}/decrease/available")]
        public async Task<IActionResult> DecreaseAvailable(int bookId)
        {
            await _inventoryService.DecreaseQuantityAvailable(bookId);
            return NoContent();
        }

        [HttpPost("{bookId}/decrease/sold")]
        public async Task<IActionResult> DecreaseSold(int bookId)
        {
            await _inventoryService.DecreaseQuantitySold(bookId);
            return NoContent();
        }

        [HttpPost("{bookId}/decrease/borrowed")]
        public async Task<IActionResult> DecreaseBorrowed(int bookId)
        {
            await _inventoryService.DecreaseQuantityBorrowed(bookId);
            return NoContent();
        }
    }
}