using System.Net.Http.Headers;
using Order.API.Models;
using Order.API.Models.Response;
using Order.API.Services.Interfaces;

namespace Order.API.Services
{
    public class BasketService : IBasketService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<BasketService> _logger;

        public BasketService(IHttpClientFactory httpClientFactory, ILogger<BasketService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<IEnumerable<GetBasketItemsResponse>> GetBasketByLoginAsync(string token)
        {
            var client = _httpClientFactory.CreateClient("BasketService");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.PostAsync($"{WebApiLinks.BasketApi}/Basket/GetBasketByLogin", null);

            if (response.IsSuccessStatusCode)
            {
                var basket = await response.Content.ReadFromJsonAsync<IEnumerable<GetBasketItemsResponse>>();

                return basket;
            }

            return null;
        }

        public async Task<bool> DeleteBasketByLoginAsync(string login)
        {
            var client = _httpClientFactory.CreateClient("BasketService");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", login);
            var response = await client.PostAsync($"{WebApiLinks.BasketApi}/Basket/DeleteBasketByLogin", null);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
