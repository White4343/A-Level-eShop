using System.Text;
using System.Text.Json;
using Basket.API.Models;
using Basket.API.Services.Interfaces;

namespace Basket.API.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CatalogService> _logger;

        public CatalogService(IHttpClientFactory httpClientFactory, ILogger<CatalogService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        // call for catalog.api service
        public async Task<ItemResponse> GetItemByIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("CatalogService");
            var response = await client.GetAsync($"{WebApiLinks.CatalogApi}/Item/{id}");

            if (response.IsSuccessStatusCode)
            {
                var item = await response.Content.ReadFromJsonAsync<ItemResponse>();

                return item;
            }

            return null;
        }

        public async Task<bool> PatchItemQuantityAsync(int id, int quantity)
        {
            var item = await GetItemByIdAsync(id);

            if (!IsItemAvailable(item, quantity))
            {
                return true;
            }

            var client = _httpClientFactory.CreateClient("CatalogService");
            var body = new StringContent(JsonSerializer.Serialize(new { quantity }), Encoding.UTF8, "application/json");

            var response = await client.PatchAsync($"{WebApiLinks.CatalogApi}/Item/{id}?quantity={quantity}", body);

            if (response.IsSuccessStatusCode)
            { 
                return true;
            }

            return false;
        }

        private bool IsItemAvailable(ItemResponse item, int quantity)
        {
            return item != null && item.AvailableStock >= quantity;
        }
    }
}