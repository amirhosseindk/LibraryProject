namespace Application.DTO.Book
{
    public class BookRDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public string CategoryName { get; set; }
        public string Publisher { get; set; }
        public decimal Price { get; set; }
        public double Score { get; set; }
        public string Summary { get; set; }
        public DateTime PublishYear { get; set; }
    }
}
