namespace Catalog.API.Models
{
    public class PaginatedItems<T>
    {
        public long TotalItems { get; set; }

        public IEnumerable<T> Data { get; set; } = null!;
    }
}
