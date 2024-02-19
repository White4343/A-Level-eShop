namespace Order.API.Models.Request
{
    public class CreateOrderRequest
    {
        public int Id { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
