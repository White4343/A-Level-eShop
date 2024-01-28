namespace Catalog.API.Services
{
    public class BffItemService : IBffItemService
    {
        private readonly IBffItemRepository _bffItemRepository;
        private readonly ILogger<BffItemService> _logger;
        private readonly IMapper _mapper;

        public BffItemService(IBffItemRepository bffItemRepository, ILogger<BffItemService> logger, IMapper mapper)
        {
            _bffItemRepository = bffItemRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginatedItemsResponse<ItemDto>> GetByPageAsync(int pageIndex, int pageSize, Dictionary<BffItemFilter, int>? filters)
        {
            int? brandFilter = null;
            int? typeFilter = null;

            if (filters != null)
            {
                if (filters.TryGetValue(BffItemFilter.Brand, out var brand))
                {
                    brandFilter = brand;
                }

                if (filters.TryGetValue(BffItemFilter.Type, out var type))
                {
                    typeFilter = type;
                }
            }

            var result = await _bffItemRepository.GetByPageAsync(pageIndex, pageSize, brandFilter, typeFilter);

            if (result == null)
            {
                return null;
            }

            return new PaginatedItemsResponse<ItemDto>()
            {
                Count = result.TotalItems,
                Data = result.Data.Select(s => _mapper.Map<ItemDto>(s)).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }
    }
}
