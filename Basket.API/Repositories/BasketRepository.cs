using Basket.API.Repositories.Interfaces;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<BasketRepository> _logger;
        private readonly IMapper _mapper;

        public BasketRepository(AppDbContext context, ILogger<BasketRepository> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<BasketDto> CreateBasketAsync(Data.Entities.Basket basket)
        {
            if (basket == null)
            {
                _logger.LogError("Basket object sent from client is null.");

                return null;
            }

            _context.Baskets.Add(basket);

            var basketDto = _mapper.Map<BasketDto>(basket);

            await _context.SaveChangesAsync();

            return basketDto;
        }

        public async Task<IEnumerable<BasketDto>> GetBasketsAsync()
        {
            var baskets = await _context.Baskets.ToListAsync();

            if (baskets == null)
            {
                _logger.LogError("Baskets not found.");

                return null;
            }

            var basketsDto = _mapper.Map<IEnumerable<BasketDto>>(baskets);

            return basketsDto;
        }

        public async Task<IEnumerable<BasketDto>> GetBasketByLoginAsync(string login)
        {
            var baskets = await _context.Baskets.Where(b => b.UserLogin == login).ToListAsync();

            if (baskets == null)
            {
                _logger.LogError($"Baskets with login: {login}, not found.");

                return null;
            }

            var basketsDto = _mapper.Map<IEnumerable<BasketDto>>(baskets);

            return basketsDto;
        }

        public async Task<BasketDto> GetBasketByIdAsync(int id, string login)
        {
            if (!IsAuthorOfBasketAsync(id, login).Result)
            {
                return null;
            }

            var basket = await _context.Baskets.FindAsync(id);

            var basketDto = _mapper.Map<BasketDto>(basket);

            return basketDto;
        }

        public async Task<BasketDto> UpdateBasketAsync(Data.Entities.Basket basket, string login)
        {
            if (!IsAuthorOfBasketAsync(basket.Id, login).Result)
            {
                return null;
            }

            _context.Baskets.Update(basket);

            await _context.SaveChangesAsync();

            var basketDto = _mapper.Map<BasketDto>(basket);

            return basketDto;
        }

        public async Task<bool> DeleteBasketByIdAsync(int id, string login)
        {
            if (!IsAuthorOfBasketAsync(id, login).Result)
            {
                return false;
            }

            var basket = await _context.Baskets.FindAsync(id);

            _context.Baskets.Remove(basket);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteBasketByLoginAsync(string login)
        {
            var basket = await _context.Baskets.Where(b => b.UserLogin == login).ToListAsync();

            if (basket == null)
            {
                _logger.LogError($"Basket with login: {login}, not found.");

                return false;
            }

            _context.Baskets.RemoveRange(basket);

            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<bool> IsAuthorOfBasketAsync(int basketId, string login)
        {
            if (!IsBasketExistAsync(basketId).Result)
            {
                return false;
            }

            var basket = await _context.Baskets.AsNoTracking().FirstOrDefaultAsync(b => b.Id == basketId);

            if (basket.UserLogin != login)
            {
                _logger.LogError($"Basket with id: {basketId} " +
                                                                 $"was tried to be updated by user with login: {login}, " +
                                                                                                 $"but this basket belongs to user with login: {basket.UserLogin}.");

                return false;
            }

            return true;
        }

        private async Task<bool> IsBasketExistAsync(int basketId)
        {
            var basket = await _context.Baskets.AsNoTracking().FirstOrDefaultAsync(b => b.Id == basketId);

            if (basket == null)
            {
                _logger.LogError($"Basket with id: {basketId}, not found.");

                return false;
            }

            return true;
        }
    }
}