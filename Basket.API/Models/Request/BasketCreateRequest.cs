using System.ComponentModel.DataAnnotations;

namespace Basket.API.Models.Request
{
    public class BasketCreateRequest
    {
        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal ItemPrice { get; set; }

        [Required]
        public int ItemId { get; set; }
    }
}