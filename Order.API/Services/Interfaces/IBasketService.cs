using Order.API.Models.Response;

namespace Order.API.Services.Interfaces
{
    public interface IBasketService
    {
        Task<IEnumerable<GetBasketItemsResponse>> GetBasketByLoginAsync(string token);
        Task<bool> DeleteBasketByLoginAsync(string login);
    }
}