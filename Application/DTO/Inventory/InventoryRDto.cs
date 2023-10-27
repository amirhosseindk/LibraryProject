using Application.DTO.Book;

namespace Application.DTO.Inventory
{
    public class InventoryRDto
    {
        public BookRDto Book { get; set; }
        public int QuantityAvailable { get; set; }
        public int QuantitySold { get; set; }
        public int QuantityBorrowed { get; set; }
    }
}
