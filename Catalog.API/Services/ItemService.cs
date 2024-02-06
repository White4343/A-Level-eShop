namespace Catalog.API.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILogger<ItemService> _logger;

        public ItemService(IItemRepository itemRepository, ILogger<ItemService> logger)
        {
            _itemRepository = itemRepository;
            _logger = logger;
        }

        public async Task<ItemDto> CreateItemAsync(Item item)
        {
            var itemResult = await _itemRepository.CreateItemAsync(item);

            var itemDto = new ItemDto
            {
                Name = itemResult.Name,
                Description = itemResult.Description,
                Price = itemResult.Price,
                PictureUrl = itemResult.PictureUrl,
                AvailableStock = itemResult.AvailableStock,
                TypeId = itemResult.TypeId,
                BrandId = itemResult.BrandId
            };
            
            return itemDto;
        }

        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var itemsResult = await _itemRepository.GetItemsAsync();

            var itemsDto = new List<ItemDto>();

            foreach (var item in itemsResult)
            {
                var itemDto = new ItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    PictureUrl = item.PictureUrl,
                    AvailableStock = item.AvailableStock,
                    TypeId = item.TypeId,
                    BrandId = item.BrandId
                };

                itemsDto.Add(itemDto);
            }

            return itemsDto;
        }

        public async Task<ItemDto> GetItemByIdAsync(int id)
        {
            var itemResult = await _itemRepository.GetItemByIdAsync(id);

            var itemDto = new ItemDto
            {
                Id = itemResult.Id,
                Name = itemResult.Name,
                Description = itemResult.Description,
                Price = itemResult.Price,
                PictureUrl = itemResult.PictureUrl,
                AvailableStock = itemResult.AvailableStock,
                TypeId = itemResult.TypeId,
                BrandId = itemResult.BrandId
            };

            return itemDto;
        }

        public async Task<ItemDto> UpdateItemAsync(Item item)
        {
            var itemResult = await _itemRepository.UpdateItemAsync(item);

            var itemDto = new ItemDto
            {
                Name = itemResult.Name,
                Description = itemResult.Description,
                Price = itemResult.Price,
                PictureUrl = itemResult.PictureUrl,
                AvailableStock = itemResult.AvailableStock,
                TypeId = itemResult.TypeId,
                BrandId = itemResult.BrandId
            };

            return itemDto;
        }

        public async Task<ItemDto> PatchItemQuantityAsync(int id, int quantity)
        {
            var itemResult = await _itemRepository.PatchItemQuantityAsync(id, quantity);

            var itemDto = new ItemDto
            {
                Name = itemResult.Name,
                Description = itemResult.Description,
                Price = itemResult.Price,
                PictureUrl = itemResult.PictureUrl,
                AvailableStock = itemResult.AvailableStock,
                TypeId = itemResult.TypeId,
                BrandId = itemResult.BrandId
            };

            return itemDto;
        }
        
        public async Task<bool> DeleteItemAsync(int id)
        {
            return await _itemRepository.DeleteItemAsync(id);
        }
    }
}
