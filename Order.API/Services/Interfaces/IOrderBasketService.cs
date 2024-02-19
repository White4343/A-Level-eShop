using Order.API.Models.Dtos;
using Order.API.Models.Request;

namespace Order.API.Services.Interfaces
{
    public interface IOrderBasketService
    {
        Task<OrderBasketDto> CreateOrderBasketAsync(CreateOrderBasketRequest order, string userLogin);
        Task<IEnumerable<OrderBasketDto>> CreateOrdersBasketAsync(string userLogin, string token);
        Task<OrderBasketDto> GetOrderBasketByIdAsync(int id);
        Task<IEnumerable<OrderBasketDto>> GetOrderBasketByOrderIdAsync(int id);
        Task<IEnumerable<OrderBasketDto>> GetOrdersBasketAsync();
        Task<bool> UpdateOrderBasketAsync(OrderBasketDto order);
        Task<bool> DeleteOrderBasketAsync(int id);
    }
}