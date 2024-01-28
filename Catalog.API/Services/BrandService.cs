namespace Catalog.API.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ILogger<BrandService> _logger;

        public BrandService(IBrandRepository brandRepository, ILogger<BrandService> logger)
        {
            _brandRepository = brandRepository;
            _logger = logger;
        }

        public async Task<Brand> CreateBrandAsync(Brand brand)
        {
            return await _brandRepository.CreateBrandAsync(brand);
        }

        public async Task<IEnumerable<Brand>> GetBrandsAsync()
        {
            return await _brandRepository.GetBrandsAsync();
        }

        public async Task<Brand> GetBrandByIdAsync(int id)
        {
            return await _brandRepository.GetBrandByIdAsync(id);
        }

        public async Task<Brand> UpdateBrandAsync(Brand brand)
        {
            return await _brandRepository.UpdateBrandAsync(brand);
        }

        public async Task<bool> DeleteBrandAsync(int id)
        {
            return await _brandRepository.DeleteBrandAsync(id);
        }
    }
}
