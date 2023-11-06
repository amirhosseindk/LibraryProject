using Application.Commands.Inventory;
using Application.Query.Inventory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly ISender _sender;

        public InventoriesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inventories = await _sender.Send(new GetInventoriesQuery());
            return Ok(inventories);
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetByBookId(int bookId)
        {
            var inventory = await _sender.Send(new GetInventoryQuery(bookId));

            if (inventory == null)
                return NotFound();

            return Ok(inventory);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInventoryCommand command)
        {
            var newInventoryId = await _sender.Send(command);
            return CreatedAtAction("GetByBookId", new { bookId = newInventoryId }, command);
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> Update(int bookId, UpdateInventoryCommand command)
        {
            if (bookId != command.InventoryDto.BookId)
                return BadRequest();

            await _sender.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sender.Send(new DeleteInventoryCommand(id));
            return NoContent();
        }

        //[HttpPost("{bookId}/increase/available")]
        //public async Task<IActionResult> IncreaseAvailable(int bookId)
        //{
        //    await _inventoryService.IncreaseQuantityAvailable(bookId);
        //    return NoContent();
        //}

        //[HttpPost("{bookId}/increase/sold")]
        //public async Task<IActionResult> IncreaseSold(int bookId)
        //{
        //    await _inventoryService.IncreaseQuantitySold(bookId);
        //    return NoContent();
        //}

        //[HttpPost("{bookId}/increase/borrowed")]
        //public async Task<IActionResult> IncreaseBorrowed(int bookId)
        //{
        //    await _inventoryService.IncreaseQuantityBorrowed(bookId);
        //    return NoContent();
        //}

        //[HttpPost("{bookId}/decrease/available")]
        //public async Task<IActionResult> DecreaseAvailable(int bookId)
        //{
        //    await _inventoryService.DecreaseQuantityAvailable(bookId);
        //    return NoContent();
        //}

        //[HttpPost("{bookId}/decrease/sold")]
        //public async Task<IActionResult> DecreaseSold(int bookId)
        //{
        //    await _inventoryService.DecreaseQuantitySold(bookId);
        //    return NoContent();
        //}

        //[HttpPost("{bookId}/decrease/borrowed")]
        //public async Task<IActionResult> DecreaseBorrowed(int bookId)
        //{
        //    await _inventoryService.DecreaseQuantityBorrowed(bookId);
        //    return NoContent();
        //}
    }
}