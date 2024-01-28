using Catalog.API.Models;

namespace Catalog.API.Services.Interfaces
{
    public interface IBffItemService
    {
        Task<PaginatedItemsResponse<ItemDto>> GetByPageAsync(int pageIndex, int pageSize, Dictionary<BffItemFilter, int>? filters);
    }
}
