using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Order.API.Data;
using Order.API.Repositories.Interfaces;

namespace Order.API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrderRepository> _logger;
        private readonly IMapper _mapper;

        public OrderRepository(AppDbContext context, IMapper mapper, ILogger<OrderRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Data.Entities.Order> CreateOrderAsync(Data.Entities.Order order)
        {
            if (order == null)
            {
                _logger.LogError("Order is null");
                throw new ArgumentNullException(nameof(order));
            }

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Data.Entities.Order> GetOrderByIdAsync(int id, string userLogin)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 0");
            }

            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id && o.UserLogin == userLogin);
        }

        public async Task<IEnumerable<Data.Entities.Order>> GetOrdersAsync()
        {
            var orders = await _context.Orders.ToListAsync();

            return orders;
        }

        public async Task<IEnumerable<Data.Entities.Order>> GetOrdersByUserAsync(string userLogin)
        {
            var orders = await _context.Orders.Where(o => o.UserLogin == userLogin).ToListAsync();

            return orders;
        }

        public async Task<bool> UpdateOrderAsync(Data.Entities.Order order, string login)
        {
            var orderToUpdate = await _context.Orders.FirstOrDefaultAsync(o => o.Id == order.Id && o.UserLogin == login);

            if (orderToUpdate == null)
            {
                return false;
            }

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PatchOrderAsync(int id, decimal totalPrice, string login)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 0");
            }

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id && o.UserLogin == login);

            if (order == null)
            {
                return false;
            }

            order.TotalPrice = totalPrice;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id, string login)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 0");
            }

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id && o.UserLogin == login);
            if (order == null)
            {
                return false;
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrdersByLoginAsync(string login)
        {
            var orders = await _context.Orders.Where(o => o.UserLogin == login).ToListAsync();
            if (orders == null)
            {
                return false;
            }
            _context.Orders.RemoveRange(orders);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
