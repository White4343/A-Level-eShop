namespace Order.API.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public decimal? TotalPrice { get; set; }
        
        public string UserLogin { get; set; }
    }
}