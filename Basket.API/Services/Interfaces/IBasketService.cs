namespace Basket.API.Services.Interfaces
{
    public interface IBasketService
    {
        Task<BasketDto> CreateBasketAsync(BasketCreateRequest basket, string login);
        
        Task<IEnumerable<BasketDto>> GetBasketsAsync();

        Task<IEnumerable<BasketDto>> GetBasketByLoginAsync(string login);

        Task<BasketDto> GetBasketByIdAsync(int id, string login);

        Task<BasketDto> UpdateBasketAsync(BasketUpdateRequest basket, string login);
        
        Task<bool> DeleteBasketByIdAsync(int id, string login);

        Task<bool> DeleteBasketByLoginAsync(string login);
    }
}
