namespace Basket.API.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<ItemResponse> GetItemByIdAsync(int id);
        Task<bool> PatchItemQuantityAsync(int id, int quantity);
    }
}
