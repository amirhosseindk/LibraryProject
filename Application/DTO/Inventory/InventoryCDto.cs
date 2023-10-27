namespace Application.DTO.Inventory
{
    public class InventoryCDto
    {
        public int BookId { get; set; }
        public int QuantityAvailable { get; set; }
        public int QuantitySold { get; set; }
        public int QuantityBorrowed { get; set; }
    }
}
