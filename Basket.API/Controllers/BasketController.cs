using System.Security.Claims;
using Basket.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Authorize(Policy = "Basket.Client")]
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IBasketService basketService, ILogger<BasketController> logger)
        {
            _basketService = basketService;
            _logger = logger;
        }

        [HttpPost(Name = "CreateBasket")]
        public async Task<ActionResult<BasketDto>> CreateBasketAsync([FromBody] BasketCreateRequest basket)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model object sent from client.");

                return BadRequest();
            }

            var login = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var basketResult = await _basketService.CreateBasketAsync(basket, login);

            if (basketResult == null)
            {
                _logger.LogError("Basket object sent from client is null.");

                return BadRequest();
            }

            return Ok(basket);
        }

        [Authorize(Policy = "Basket.FullAccess")]
        [HttpGet("GetBaskets")]
        public async Task<ActionResult<IEnumerable<BasketDto>>> GetBasketsAsync()
        {
            var baskets = await _basketService.GetBasketsAsync();

            if (baskets == null)
            {
                _logger.LogError("Baskets not found.");

                return NotFound();
            }

            return Ok(baskets);
        }

        [HttpPost("GetBasketByLogin")]
        public async Task<ActionResult<IEnumerable<BasketDto>>> GetBasketByLoginAsync()
        {
            var login = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var baskets = await _basketService.GetBasketByLoginAsync(login);

            if (baskets == null)
            {
                _logger.LogError($"Baskets with login: {login}, not found.");

                return NotFound();
            }

            return Ok(baskets);
        }

        [Authorize(Policy = "Basket.FullAccess")]
        [HttpGet("{id}", Name = "GetBasketById")]
        public async Task<ActionResult<BasketDto>> GetBasketByIdAsync(int id)
        {
            var login = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var basket = await _basketService.GetBasketByIdAsync(id, login);

            if (basket == null)
            {
                _logger.LogError($"Basket with id: {id}, not found.");

                return NotFound();
            }

            return Ok(basket);
        }

        [HttpPut(Name = "UpdateBasket")]
        public async Task<ActionResult<BasketDto>> UpdateBasketAsync([FromBody] BasketUpdateRequest basket)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model object sent from client.");

                return BadRequest();
            }

            var login = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var basketResult = await _basketService.UpdateBasketAsync(basket, login);

            if (basketResult == null)
            {
                _logger.LogError("Basket object sent from client is null.");

                return BadRequest();
            }

            return Ok(basket);
        }

        [HttpDelete("{id}", Name = "DeleteBasketById")]
        public async Task<ActionResult<bool>> DeleteBasketByIdAsync(int id)
        {
            var login = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _basketService.DeleteBasketByIdAsync(id, login);

            if (!result)
            {
                _logger.LogError($"Basket with id: {id}, not found.");

                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("DeleteBasketByLogin")]
        public async Task<ActionResult<bool>> DeleteBasketByLoginAsync()
        {
            var login = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _basketService.DeleteBasketByLoginAsync(login);

            if (!result)
            {
                _logger.LogError($"Basket with login: {login}, not found.");

                return NotFound();
            }

            return Ok(result);
        }
    }
}
