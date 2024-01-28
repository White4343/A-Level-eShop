namespace Catalog.API.Controllers
{
    [Authorize(Policy = "Catalog.FullAccess")]
    [ApiController]
    [Route("[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly ILogger<BrandController> _logger;

        public BrandController(IBrandService brandService, ILogger<BrandController> logger)
        {
            _brandService = brandService;
            _logger = logger;
        }

        [HttpPost(Name = "CreateBrand")]
        public async Task<ActionResult<Brand>> Post([FromBody] Brand brand)
        {
            var createdBrand = await _brandService.CreateBrandAsync(brand);

            if (createdBrand == null)
            {
                return BadRequest();
            }

            return Ok(CreatedAtRoute("GetBrandById", new { id = createdBrand.Id }, createdBrand));
        }

        [HttpGet(Name = "GetBrands")]
        public async Task<ActionResult<IEnumerable<Brand>>> Get()
        {
            var brands = await _brandService.GetBrandsAsync();

            if (brands == null)
            {
                return NotFound();
            }

            return Ok(brands);
        }

        [HttpGet("{id}", Name = "GetBrandById")]
        public async Task<ActionResult<Brand>> Get(int id)
        {
            var brand = await _brandService.GetBrandByIdAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }

        [HttpPut(Name = "UpdateBrand")]
        public async Task<ActionResult<bool>> Put([FromBody] Brand brand)
        {
            var updatedBrand = await _brandService.UpdateBrandAsync(brand);

            if (updatedBrand == null)
            {
                return NotFound();
            }

            return Ok(updatedBrand);
        }

        [HttpDelete("{id}", Name = "DeleteBrand")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _brandService.DeleteBrandAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
