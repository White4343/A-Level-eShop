using Basket.API.Repositories.Interfaces;
using Basket.API.Services.Interfaces;

namespace Basket.API.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ICatalogService _catalogService;
        private readonly ILogger<BasketService> _logger;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, ICatalogService catalogService, ILogger<BasketService> logger, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _catalogService = catalogService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<BasketDto> CreateBasketAsync(BasketCreateRequest basket, string login)
        {
            var item = await IsItemAvailable(basket.ItemId, basket.Quantity);

            if (!item)
            {
                _logger.LogError("Item not available.");

                return null;
            }

            var basketEntity = new Data.Entities.Basket
            {
                CreatedAt = DateTime.UtcNow,
                Quantity = basket.Quantity,
                ItemPrice = basket.ItemPrice,
                ItemId = basket.ItemId,
                UserLogin = login
            };

            /*var itemQuantity = await PatchItemQuantity(basket.ItemId, basket.Quantity);

            if (itemQuantity == false)
            {
                _logger.LogError("Item not available.");

                return null;
            }*/

            return await _basketRepository.CreateBasketAsync(basketEntity);
        }

        public async Task<IEnumerable<BasketDto>> GetBasketsAsync()
        {
            return await _basketRepository.GetBasketsAsync();
        }

        public async Task<IEnumerable<BasketDto>> GetBasketByLoginAsync(string login)
        {
            return await _basketRepository.GetBasketByLoginAsync(login);
        }

        public async Task<BasketDto> GetBasketByIdAsync(int id, string login)
        {
            return await _basketRepository.GetBasketByIdAsync(id, login);
        }

        public async Task<BasketDto> UpdateBasketAsync(BasketUpdateRequest basket, string login)
        {
            var item = await IsItemAvailable(basket.ItemId, basket.Quantity);

            if (!item)
            {
                _logger.LogError("Item not available.");

                return null;
            }

            var basketEntity = new Data.Entities.Basket
            {
                Id = basket.Id,
                Quantity = basket.Quantity,
                ItemPrice = basket.ItemPrice,
                ItemId = basket.ItemId,
                UserLogin = login
            };

            //var itemQuantity = await PatchItemQuantity(basket.ItemId, basket.Quantity);

            //if (itemQuantity == false)
            //{
            //    _logger.LogError("Item not available.");

            //    return null;
            //}

            return await _basketRepository.UpdateBasketAsync(basketEntity, login);
        }

        public async Task<bool> DeleteBasketByIdAsync(int id, string login)
        {
            return await _basketRepository.DeleteBasketByIdAsync(id, login);
        }

        public async Task<bool> DeleteBasketByLoginAsync(string login)
        {
            return await _basketRepository.DeleteBasketByLoginAsync(login);
        }

        private async Task<bool> IsItemAvailable(int itemId, int quantity)
        {
            var item = await _catalogService.GetItemByIdAsync(itemId);

            return item != null && item.AvailableStock >= quantity;
        }

        private async Task<bool> PatchItemQuantity(int itemId, int quantity)
        {
            var item = await _catalogService.PatchItemQuantityAsync(itemId, quantity);

            return item;
        }
    }
}
