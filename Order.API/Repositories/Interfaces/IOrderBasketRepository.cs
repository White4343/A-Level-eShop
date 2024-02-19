using Order.API.Models.Dtos;
using Order.API.Models.Request;

namespace Order.API.Repositories.Interfaces
{
    public interface IOrderBasketRepository
    {
        Task<OrderBasketDto> CreateOrderBasketAsync(OrderBasketDto order);
        Task<IEnumerable<OrderBasketDto>> CreateOrdersBasketAsync(IEnumerable<OrderBasketDto> orders);
        Task<OrderBasketDto> GetOrderBasketByIdAsync(int id);
        Task<IEnumerable<OrderBasketDto>> GetOrderBasketByOrderIdAsync(int id);
        Task<IEnumerable<OrderBasketDto>> GetOrdersBasketAsync();
        Task<bool> UpdateOrderBasketAsync(OrderBasketDto order);
        Task<bool> DeleteOrderBasketAsync(int id);
    }
}