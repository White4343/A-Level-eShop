using Type = Catalog.API.Data.Entities.Type;

namespace Catalog.API.Data
{
    public class DbInnit
    {
        public static async Task InnitAsync(AppDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!await context.Brands.AnyAsync())
            {
                await context.Brands.AddRangeAsync(GetPreconfiguredBrands());
                
                await context.SaveChangesAsync();
            }

            if (!await context.Types.AnyAsync())
            {
                await context.Types.AddRangeAsync(GetPreconfiguredTypes());

                await context.SaveChangesAsync();
            }

            if (!await context.Items.AnyAsync())
            {
                await context.Items.AddRangeAsync(GetPreconfiguredItems());

                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<Brand> GetPreconfiguredBrands()
        {
            return new List<Brand>
            {
                new Brand { Name = "Brand 1" },
                new Brand { Name = "Brand 2" },
                new Brand { Name = "Brand 3" },
                new Brand { Name = "Brand 4" },
                new Brand { Name = "Brand 5" },
                new Brand { Name = "Brand 6" },
                new Brand { Name = "Brand 7" },
                new Brand { Name = "Brand 8" },
                new Brand { Name = "Brand 9" },
                new Brand { Name = "Brand 10" },
                new Brand { Name = "Brand 11" },
                new Brand { Name = "Brand 12" },
                new Brand { Name = "Brand 13" },
                new Brand { Name = "Brand 14" },
                new Brand { Name = "Brand 15" },
            };
        }

        private static IEnumerable<Type> GetPreconfiguredTypes()
        {
            return new List<Type>
            {
                new Type { Name = "Type 1" },
                new Type { Name = "Type 2" },
                new Type { Name = "Type 3" },
                new Type { Name = "Type 4" },
                new Type { Name = "Type 5" },
                new Type { Name = "Type 6" },
                new Type { Name = "Type 7" },
                new Type { Name = "Type 8" },
                new Type { Name = "Type 9" },
                new Type { Name = "Type 10" },
                new Type { Name = "Type 11" },
                new Type { Name = "Type 12" },
                new Type { Name = "Type 13" },
                new Type { Name = "Type 14" },
                new Type { Name = "Type 15" },
            };
        }

        private static IEnumerable<Item> GetPreconfiguredItems()
        {
            return new List<Item>
            {
                new Item
                {
                    Name = "Item 1", Price = 5, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/1",
                    Description = "Description for Item 1", AvailableStock = 100, TypeId = 1, BrandId = 1
                },
                new Item
                {
                    Name = "Item 2", Price = 10, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/2",
                    Description = "Description for Item 2", AvailableStock = 100, TypeId = 2, BrandId = 2
                },
                new Item
                {
                    Name = "Item 3", Price = 15, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/3",
                    Description = "Description for Item 3", AvailableStock = 100, TypeId = 3, BrandId = 3
                },
                new Item
                {
                    Name = "Item 4", Price = 20, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/4",
                    Description = "Description for Item 4", AvailableStock = 100, TypeId = 4, BrandId = 4
                },
                new Item
                {
                    Name = "Item 5", Price = 25, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/5",
                    Description = "Description for Item 5", AvailableStock = 100, TypeId = 5, BrandId = 5
                },
                new Item
                {
                    Name = "Item 6", Price = 30, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/6",
                    Description = "Description for Item 6", AvailableStock = 100, TypeId = 6, BrandId = 6
                },
                new Item
                {
                    Name = "Item 7", Price = 35, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/7",
                    Description = "Description for Item 7", AvailableStock = 100, TypeId = 7, BrandId = 7
                },
            };
        }
    }
}
