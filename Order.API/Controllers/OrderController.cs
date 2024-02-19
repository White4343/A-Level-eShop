using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.API.Models.Request;
using Order.API.Services.Interfaces;
using System.Security.Claims;

namespace Order.API.Controllers
{
    [Authorize(Policy = "Order.Client")]
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }
        
        [HttpPost(Name = "CreateOrder")]
        public async Task<ActionResult<Data.Entities.Order>> CreateOrderAsync()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model object sent from client.");

                return BadRequest();
            }

            var login = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orderResult = await _orderService.CreateOrderAsync(login, 0);

            if (orderResult == null)
            {
                _logger.LogError("Order object sent from client is null.");

                return BadRequest();
            }

            return Ok(orderResult);
        }

        [Authorize(Policy = "Order.FullAccess")]
        [HttpGet(Name = "GetOrders")]
        public async Task<ActionResult<IEnumerable<Data.Entities.Order>>> GetOrdersAsync()
        {
            var orders = await _orderService.GetOrdersAsync();

            if (orders == null)
            {
                _logger.LogError("Orders not found.");

                return NotFound();
            }

            return Ok(orders);
        }

        [HttpGet("GetOrdersByUser")]
        public async Task<ActionResult<IEnumerable<Data.Entities.Order>>> GetOrdersByUserAsync()
        {
            var login = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orders = await _orderService.GetOrdersByUserAsync(login);

            if (orders == null)
            {
                _logger.LogError("Orders not found.");

                return NotFound();
            }

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Data.Entities.Order>> GetOrderByIdAsync(int id)
        {
            string login = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = await _orderService.GetOrderByIdAsync(id, login);

            if (order == null)
            {
                _logger.LogError("Order not found.");

                return NotFound();
            }

            return Ok(order);
        }

        [HttpPut(Name = "UpdateOrder")]
        public async Task<ActionResult<Data.Entities.Order>> UpdateOrderAsync([FromBody] Data.Entities.Order order)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model object sent from client.");

                return BadRequest();
            }

            var login = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orderResult = await _orderService.UpdateOrderAsync(order, login);

            if (orderResult == null)
            {
                _logger.LogError("Order object sent from client is null.");

                return BadRequest();
            }

            return Ok(order);
        }

        [Authorize(Policy = "Order.FullAccess")]
        [HttpDelete("{id}", Name = "DeleteOrder")]
        public async Task<ActionResult<bool>> DeleteOrderAsync(int id)
        {
            var login = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = await _orderService.DeleteOrderAsync(id, login);

            if (!order)
            {
                _logger.LogError("Order not found.");

                return NotFound();
            }

            return Ok(order);
        }
    }
}
