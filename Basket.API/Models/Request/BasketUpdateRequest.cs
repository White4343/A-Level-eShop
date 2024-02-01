using System.ComponentModel.DataAnnotations;

namespace Basket.API.Models.Request
{
    public class BasketUpdateRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal ItemPrice { get; set; }

        [Required]
        public int ItemId { get; set; }
    }
}
