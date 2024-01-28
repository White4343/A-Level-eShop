namespace Catalog.API.Data.Entities
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string PictureUrl { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int AvailableStock { get; set; }

        public int TypeId { get; set; }

        public Type Type { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }
    }
}
