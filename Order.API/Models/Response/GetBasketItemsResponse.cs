namespace Order.API.Models.Response
{
    public class GetBasketItemsResponse
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public int Quantity { get; set; }

        public decimal ItemPrice { get; set; }

        public int ItemId { get; set; }

        public string UserLogin { get; set; }
    }
}