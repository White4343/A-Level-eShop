using Catalog.API.Models;

namespace Catalog.API.Repositories
{
    public class BffItemRepository : IBffItemRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<BffItemRepository> _logger;

        public BffItemRepository(AppDbContext context, ILogger<BffItemRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PaginatedItems<Item>> GetByPageAsync(int pageIndex, int pageSize, int? brandFilter,
            int? typeFilter)
        {
            IQueryable<Item> query = _context.Items;

            if (brandFilter.HasValue)
            {
                query = query.Where(w => w.BrandId == brandFilter.Value);
            }

            if (typeFilter.HasValue)
            {
                query = query.Where(w => w.TypeId == typeFilter.Value);
            }

            var totalItems = await query.LongCountAsync();

            var itemsOnPage = await query
                .Include(i => i.Brand)
                .Include(i => i.Type)
                .OrderBy(o => o.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItems<Item>() { TotalItems = totalItems, Data = itemsOnPage };
        }
    }
}
