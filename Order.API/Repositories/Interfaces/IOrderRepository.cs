namespace Order.API.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Data.Entities.Order> CreateOrderAsync(Data.Entities.Order order);
        Task<Data.Entities.Order> GetOrderByIdAsync(int id, string userLogin);
        Task<IEnumerable<Data.Entities.Order>> GetOrdersAsync();
        Task<IEnumerable<Data.Entities.Order>> GetOrdersByUserAsync(string userLogin);
        Task<bool> UpdateOrderAsync(Data.Entities.Order order, string login);
        Task<bool> PatchOrderAsync(int id, decimal totalPrice, string login);
        Task<bool> DeleteOrderAsync(int id, string login);
        Task<bool> DeleteOrdersByLoginAsync(string login);
    }
}