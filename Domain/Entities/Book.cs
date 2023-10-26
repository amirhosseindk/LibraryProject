namespace Domain.Entities
{
    public class Book
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Author Author { get; set; }
        public Category Category { get; set; }
        public string Publisher { get; set; }
        public decimal Price { get; set; }
        public double Score { get; set; }
        public string Summary { get; set; }
        public DateTime PublishYear { get; set; }
    }

}
