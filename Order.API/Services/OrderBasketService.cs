using AutoMapper;
using Order.API.Data.Entities;
using Order.API.Models.Dtos;
using Order.API.Models.Request;
using Order.API.Repositories.Interfaces;
using Order.API.Services.Interfaces;
using System.Transactions;

namespace Order.API.Services
{
    public class OrderBasketService : IOrderBasketService
    {
        private readonly IOrderBasketRepository _orderBasketRepository;
        private readonly IOrderService _orderService;
        private readonly IBasketService _basketService;
        private readonly ILogger<OrderBasketService> _logger;
        private readonly IMapper _mapper;

        public OrderBasketService(IOrderBasketRepository orderBasketRepository, IOrderService orderService, IBasketService basketService,
            ILogger<OrderBasketService> logger, IMapper mapper)
        {
            _orderBasketRepository = orderBasketRepository;
            _orderService = orderService;
            _basketService = basketService;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<OrderBasketDto> CreateOrderBasketAsync(CreateOrderBasketRequest order, string userLogin)
        {
            if (order == null)
            {
                _logger.LogError("Order is null");
                throw new ArgumentNullException(nameof(order));
            }

            var createdOrder = await CreateOrderAsync(userLogin, 0);

            var orderEntity = new OrderBasketDto
            {
                Quantity = order.Quantity,
                ItemPrice = order.ItemPrice,
                ItemId = order.ItemId,
                OrderId = createdOrder.Id,
            };
            
            await _orderBasketRepository.CreateOrderBasketAsync(orderEntity);

            return _mapper.Map<OrderBasketDto>(orderEntity);
        }

        public async Task<IEnumerable<OrderBasketDto>> CreateOrdersBasketAsync(string userLogin, string token)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var orders = await _basketService.GetBasketByLoginAsync(token);

                    if (orders == null)
                    {
                        _logger.LogError("Orders are null");
                        throw new ArgumentNullException(nameof(orders));
                    }

                    var totalPrice = orders.Sum(order => order.Quantity * order.ItemPrice);

                    var createdOrder = await CreateOrderAsync(userLogin, totalPrice);

                    var ordersEntity = orders.Select(order => new OrderBasketDto
                    {
                        Quantity = order.Quantity,
                        ItemPrice = order.ItemPrice,
                        ItemId = order.ItemId,
                        OrderId = createdOrder.Id,
                    });

                    var orderBaskets = await _orderBasketRepository.CreateOrdersBasketAsync(ordersEntity);

                    if (orderBaskets == null)
                    {
                        _logger.LogError("BasketOrders are null");
                        throw new ArgumentNullException(nameof(orderBaskets));
                    }

                    //await _orderService.PatchOrderAsync(createdOrder.Id, totalPrice, userLogin);

                    var deleteBasketItems = await _basketService.DeleteBasketByLoginAsync(token);

                    if (!deleteBasketItems)
                    {
                        _logger.LogError("Basket items not deleted.");
                        throw new ArgumentNullException(nameof(deleteBasketItems));
                    }

                    scope.Complete();

                    return _mapper.Map<IEnumerable<OrderBasketDto>>(ordersEntity);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Transaction failed: " + ex.Message);
                    throw ex;
                }
            }
        }


        public async Task<OrderBasketDto> GetOrderBasketByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 0");
            }

            return await _orderBasketRepository.GetOrderBasketByIdAsync(id);
        }

        public async Task<IEnumerable<OrderBasketDto>> GetOrderBasketByOrderIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 0");
            }

            return await _orderBasketRepository.GetOrderBasketByOrderIdAsync(id);
        }
        
        public async Task<IEnumerable<OrderBasketDto>> GetOrdersBasketAsync()
        {
            return await _orderBasketRepository.GetOrdersBasketAsync();
        }

        public async Task<bool> UpdateOrderBasketAsync(OrderBasketDto order)
        {
            if (order == null)
            {
                _logger.LogError("Order is null");
                throw new ArgumentNullException(nameof(order));
            }

            return await _orderBasketRepository.UpdateOrderBasketAsync(order);
        }

        public async Task<bool> DeleteOrderBasketAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 0");
            }

            return await _orderBasketRepository.DeleteOrderBasketAsync(id);
        }

        private async Task<Data.Entities.Order> CreateOrderAsync(string userLogin, decimal totalPrice)
        {
            if (userLogin == null)
            {
                _logger.LogError("Order is null");
                throw new ArgumentNullException(nameof(userLogin));
            }

            return await _orderService.CreateOrderAsync(userLogin, totalPrice);
        }
    }
}