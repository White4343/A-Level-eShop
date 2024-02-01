namespace Basket.API.Models.Dtos
{
    public class BasketDto
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public int Quantity { get; set; }

        public decimal ItemPrice { get; set; }

        public int ItemId { get; set; }
    }
}
