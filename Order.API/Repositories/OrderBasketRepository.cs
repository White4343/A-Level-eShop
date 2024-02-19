using AutoMapper;
using Basket.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Order.API.Data;
using Order.API.Data.Entities;
using Order.API.Models.Dtos;
using Order.API.Models.Request;
using Order.API.Repositories.Interfaces;

namespace Order.API.Repositories
{
    public class OrderBasketRepository : IOrderBasketRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrderBasketRepository> _logger;
        private readonly IMapper _mapper;

        public OrderBasketRepository(AppDbContext context, IMapper mapper, ILogger<OrderBasketRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OrderBasketDto> CreateOrderBasketAsync(OrderBasketDto order)
        {
            if (order == null)
            {
                _logger.LogError("Order is null");
                throw new ArgumentNullException(nameof(order));
            }

            var orderEntity = _mapper.Map<OrderBasket>(order);

            await _context.OrderBaskets.AddAsync(orderEntity);

            await _context.SaveChangesAsync();

            return _mapper.Map<OrderBasketDto>(orderEntity);
        }
        
        public async Task<IEnumerable<OrderBasketDto>> CreateOrdersBasketAsync(IEnumerable<OrderBasketDto> orders)
        {
            if (orders == null)
            {
                _logger.LogError("Orders are null");
                throw new ArgumentNullException(nameof(orders));
            }

            var ordersEntity = _mapper.Map<IEnumerable<OrderBasket>>(orders);

            await _context.OrderBaskets.AddRangeAsync(ordersEntity);

            await _context.SaveChangesAsync();

            return _mapper.Map<IEnumerable<OrderBasketDto>>(ordersEntity);
        }

        public async Task<OrderBasketDto> GetOrderBasketByIdAsync(int id)
        {
            var order = await _context.OrderBaskets.FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                _logger.LogError($"Order with id: {id}, not found.");
                return null;
            }

            return _mapper.Map<OrderBasketDto>(order);
        }

        public async Task<IEnumerable<OrderBasketDto>> GetOrderBasketByOrderIdAsync(int id)
        {
            var orders = await _context.OrderBaskets.Where(o => o.OrderId == id).ToListAsync();

            if (orders == null)
            {
                _logger.LogError($"Orders with order id: {id}, not found.");
                return null;
            }

            return _mapper.Map<IEnumerable<OrderBasketDto>>(orders);
        }


        public async Task<IEnumerable<OrderBasketDto>> GetOrdersBasketAsync()
        {
            var orders = await _context.OrderBaskets.ToListAsync();

            if (orders == null)
            {
                _logger.LogError("Orders not found.");
                return null;
            }

            return _mapper.Map<IEnumerable<OrderBasketDto>>(orders);
        }


        public async Task<bool> UpdateOrderBasketAsync(OrderBasketDto order)
        {
            if (order == null)
            {
                _logger.LogError("Order is null");
                throw new ArgumentNullException(nameof(order));
            }

            var orderEntity = _mapper.Map<OrderBasket>(order);

            _context.OrderBaskets.Update(orderEntity);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteOrderBasketAsync(int id)
        {
            var order = await _context.OrderBaskets.FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                _logger.LogError($"Order with id: {id}, not found.");
                return false;
            }

            _context.OrderBaskets.Remove(order);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}