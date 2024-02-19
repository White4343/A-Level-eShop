using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.API.Models.Dtos;
using Order.API.Models.Request;
using Order.API.Services.Interfaces;

namespace Order.API.Controllers
{
    [Authorize(Policy = "Order.Client")]
    [ApiController]
    [Route("[controller]")]
    public class OrderBasketController : ControllerBase
    {
        private readonly IOrderBasketService _orderBasketService;
        private readonly ILogger<OrderBasketController> _logger;

        public OrderBasketController(IOrderBasketService orderBasketService, ILogger<OrderBasketController> logger)
        {
            _orderBasketService = orderBasketService;
            _logger = logger;
        }

        [Authorize(Policy = "Order.FullAccess")]
        [HttpPost("CreateOrderBasket")]
        public async Task<ActionResult<OrderBasketDto>> CreateOrderBasketAsync(
            [FromBody] CreateOrderBasketRequest order)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model object sent from client.");

                return BadRequest();
            }

            var login = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orderBasketResult = await _orderBasketService.CreateOrderBasketAsync(order, login);

            if (orderBasketResult == null)
            {
                _logger.LogError("OrderBasket object sent from client is null.");

                return BadRequest();
            }

            return Ok(orderBasketResult);
        }

        [HttpPost(Name = "CreateOrdersBasket")]
        public async Task<ActionResult<IEnumerable<OrderBasketDto>>> CreateOrdersBasketAsync()
        {
            var login = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var jwtToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            var ordersBasketResult = await _orderBasketService.CreateOrdersBasketAsync(login, jwtToken);

            if (ordersBasketResult == null)
            {
                _logger.LogError("OrdersBasket object sent from client is null.");

                return BadRequest();
            }

            return Ok(ordersBasketResult);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<OrderBasketDto>>>
            GetOrdersBasketByIdAsync(int id)
        {
            var login = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var ordersBasketResult = await _orderBasketService.GetOrderBasketByIdAsync(id);

            if (ordersBasketResult == null)
            {
                _logger.LogError("OrdersBasket object sent from client is null.");

                return BadRequest();
            }

            return Ok(ordersBasketResult);
        }

        [HttpGet("GetOrderBasketByOrderId/{id}")]
        public async Task<ActionResult<IEnumerable<OrderBasketDto>>>
            GetOrderBasketByOrderIdAsync(int id)
        {
            var ordersBasketResult = await _orderBasketService.GetOrderBasketByOrderIdAsync(id);

            if (ordersBasketResult == null)
            {
                _logger.LogError("OrdersBasket object sent from client is null.");

                return BadRequest();
            }

            return Ok(ordersBasketResult);
        }
    }
}