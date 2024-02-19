namespace Order.API.Data.Entities
{
    public class OrderBasket
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public int Quantity { get; set; }

        public decimal ItemPrice { get; set; }

        public int ItemId { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
