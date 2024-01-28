using Catalog.API.Models;

namespace Catalog.API.Repositories.Interfaces
{
    public interface IBffItemRepository
    {
        Task<PaginatedItems<Item>> GetByPageAsync(int pageIndex, int pageSize, int? brandFilter, int? typeFilter);
    }
}
