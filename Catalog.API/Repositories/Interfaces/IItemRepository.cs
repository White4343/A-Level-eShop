﻿namespace Catalog.API.Repositories.Interfaces
{
    public interface IItemRepository
    {
        Task<Item> CreateItemAsync(Item item);

        Task<IEnumerable<Item>> GetItemsAsync();

        Task<Item> GetItemByIdAsync(int id);

        Task<Item> UpdateItemAsync(Item item);

        Task<Item> PatchItemQuantityAsync(int id, int quantity);

        Task<bool> DeleteItemAsync(int id);
    }
}
