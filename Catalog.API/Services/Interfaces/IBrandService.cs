namespace Catalog.API.Services.Interfaces
{
    public interface IBrandService
    {
        Task<Brand> CreateBrandAsync(Brand brand);

        Task<IEnumerable<Brand>> GetBrandsAsync();

        Task<Brand> GetBrandByIdAsync(int id);

        Task<Brand> UpdateBrandAsync(Brand brand);

        Task<bool> DeleteBrandAsync(int id);
    }
}
