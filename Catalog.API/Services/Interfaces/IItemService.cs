namespace Catalog.API.Services.Interfaces
{
    public interface IItemService
    {
        Task<ItemDto> CreateItemAsync(Item item);

        Task<IEnumerable<ItemDto>> GetItemsAsync();

        Task<ItemDto> GetItemByIdAsync(int id);

        Task<ItemDto> UpdateItemAsync(Item item);

        Task<bool> DeleteItemAsync(int id);
    }
}
