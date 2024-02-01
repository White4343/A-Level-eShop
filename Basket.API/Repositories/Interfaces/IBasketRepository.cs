namespace Basket.API.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Task<BasketDto> CreateBasketAsync(Data.Entities.Basket basket);

        Task<IEnumerable<BasketDto>> GetBasketsAsync();

        Task<IEnumerable<BasketDto>> GetBasketByLoginAsync(string login);

        Task<BasketDto> GetBasketByIdAsync(int id, string login);

        Task<BasketDto> UpdateBasketAsync(Data.Entities.Basket basket, string login);
        
        Task<bool> DeleteBasketByIdAsync(int id, string login);

        Task<bool> DeleteBasketByLoginAsync(string login);
    }
}
