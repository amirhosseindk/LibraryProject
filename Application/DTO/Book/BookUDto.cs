namespace Application.DTO.Book
{
    public class BookUDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public string Publisher { get; set; }
        public decimal Price { get; set; }
        public string Summary { get; set; }
        public double Score { get; set; }
        public DateTime PublishYear { get; set; }
    }
}