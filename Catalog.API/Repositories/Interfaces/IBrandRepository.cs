namespace Catalog.API.Repositories.Interfaces
{
    public interface IBrandRepository
    {
        Task<Brand> CreateBrandAsync(Brand brand);

        Task<IEnumerable<Brand>> GetBrandsAsync();

        Task<Brand> GetBrandByIdAsync(int id);

        Task<Brand> UpdateBrandAsync(Brand brand);

        Task<bool> DeleteBrandAsync(int id);
    }
}
