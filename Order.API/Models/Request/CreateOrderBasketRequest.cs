using System.ComponentModel.DataAnnotations;

namespace Order.API.Models.Request
{
    public class CreateOrderBasketRequest
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal ItemPrice { get; set; }

        [Required]
        public int ItemId { get; set; }
    }
}