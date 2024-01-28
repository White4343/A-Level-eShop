namespace Catalog.API.Models.Dtos
{
    public class ItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; }

        public string Description { get; set; }

        public int AvailableStock { get; set; }

        public int TypeId { get; set; }

        public int BrandId { get; set; }
    }
}
