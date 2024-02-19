using AutoMapper;
using Order.API.Models.Request;
using Order.API.Repositories.Interfaces;
using Order.API.Services.Interfaces;
using Order = Order.API.Data.Entities.Order;

namespace Order.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderService> _logger;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, ILogger<OrderService> logger, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _mapper = mapper;
        }

        
        public async Task<Data.Entities.Order> CreateOrderAsync(string userLogin, decimal? totalPrice)
        {
            if (userLogin == null)
            {
                _logger.LogError("Order is null");
                throw new ArgumentNullException(nameof(userLogin));
            }

            var orderEntity = new Data.Entities.Order()
            {
                Status = "In progress",
                CreatedAt = DateTime.UtcNow,
                UserLogin = userLogin,
                TotalPrice = totalPrice
            };

            return await _orderRepository.CreateOrderAsync(orderEntity);
        }

        public async Task<Data.Entities.Order> GetOrderByIdAsync(int id, string userLogin)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 0");
            }

            return await _orderRepository.GetOrderByIdAsync(id, userLogin);
        }

        public async Task<IEnumerable<Data.Entities.Order>> GetOrdersAsync()
        {
            return await _orderRepository.GetOrdersAsync();
        }

        public async Task<IEnumerable<Data.Entities.Order>> GetOrdersByUserAsync(string userLogin)
        {
            return await _orderRepository.GetOrdersByUserAsync(userLogin);
        }

        public async Task<bool> UpdateOrderAsync(Data.Entities.Order order, string login)
        {
            var orderToUpdate = await _orderRepository.GetOrderByIdAsync(order.Id, login);

            if (orderToUpdate == null)
            {
                _logger.LogError($"Order with id: {order.Id} was not found");
                return false;
            }

            if (orderToUpdate.UserLogin != login)
            {
                _logger.LogError($"Order with id: {order.Id} was not found for user: {login}");
                return false;
            }

            return await _orderRepository.UpdateOrderAsync(order, login);
        }

        public async Task<bool> PatchOrderAsync(int id, decimal totalPrice, string login)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 0");
            }

            return await _orderRepository.PatchOrderAsync(id, totalPrice, login);
        }

        public async Task<bool> DeleteOrderAsync(int id, string login)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 0");
            }

            return await _orderRepository.DeleteOrderAsync(id, login);
        }

        public async Task<bool> DeleteOrdersByLoginAsync(string login)
        {
            return await _orderRepository.DeleteOrdersByLoginAsync(login);
        }
    }
}
