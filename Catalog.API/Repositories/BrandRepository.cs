namespace Catalog.API.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<BrandRepository> _logger;

        public BrandRepository(AppDbContext context, ILogger<BrandRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Brand> CreateBrandAsync(Brand brand)
        {
            if (brand == null)
            {
                _logger.LogError("Brand object sent from client is null.");

                return null;
            }

            _context.Brands.Add(brand);

            await _context.SaveChangesAsync();

            return brand;
        }

        public async Task<IEnumerable<Brand>> GetBrandsAsync()
        {
            var brands = await _context.Brands.ToListAsync();

            if (brands == null)
            {
                _logger.LogError("Brands not found.");

                return null;
            }

            return brands;
        }

        public async Task<Brand> GetBrandByIdAsync(int id)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null)
            {
                _logger.LogError($"Brand with id: {id}, not found.");

                return null;
            }

            return brand;
        }

        public async Task<Brand> UpdateBrandAsync(Brand brand)
        {
            if (brand == null)
            {
                _logger.LogError("Brand object sent from client is null.");

                return null;
            }

            var brandToUpdate = await _context.Brands.FindAsync(brand.Id);

            if (brandToUpdate == null)
            {
                _logger.LogError($"Brand with id: {brand.Id}, not found.");

                return null;
            }

            _context.Update(brand);

            await _context.SaveChangesAsync();

            return brand;
        }

        public async Task<bool> DeleteBrandAsync(int id)
        {
            var brandToDelete = await _context.Brands.FindAsync(id);

            if (brandToDelete == null)
            {
                _logger.LogError($"Brand with id: {id}, not found.");

                return false;
            }

            _context.Brands.Remove(brandToDelete);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
