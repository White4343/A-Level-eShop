using Type = Catalog.API.Data.Entities.Type;

namespace Catalog.API.Controllers
{
    [Authorize(Policy = "Catalog.FullAccess")]
    [ApiController]
    [Route("[controller]")]
    public class TypeController : ControllerBase
    {
        private readonly ITypeService _typeService;
        private readonly ILogger<TypeController> _logger;

        public TypeController(ITypeService typeService, ILogger<TypeController> logger)
        {
            _typeService = typeService;
            _logger = logger;
        }

        [HttpPost(Name = "CreateType")]
        public async Task<ActionResult<Type>> Post([FromBody] Type type)
        {
            var createdType = await _typeService.CreateTypeAsync(type);

            if (createdType == null)
            {
                return BadRequest();
            }

            return Ok(CreatedAtRoute("GetTypeById", new { id = createdType.Id }, createdType));
        }

        [HttpGet(Name = "GetTypes")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Type>>> Get()
        {
            var types = await _typeService.GetTypesAsync();

            if (types == null)
            {
                return NotFound();
            }

            return Ok(types);
        }

        [HttpGet("{id}", Name = "GetTypeById")]
        [AllowAnonymous]
        public async Task<ActionResult<Type>> Get(int id)
        {
            var type = await _typeService.GetTypeByIdAsync(id);

            if (type == null)
            {
                return NotFound();
            }

            return Ok(type);
        }

        [HttpPut(Name = "UpdateType")]
        public async Task<ActionResult<bool>> Put([FromBody] Type type)
        {
            var updated = await _typeService.UpdateTypeAsync(type);

            if (updated == null)
            {
                return BadRequest();
            }

            return Ok(updated);
        }

        [HttpDelete("{id}", Name = "DeleteType")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var deleted = await _typeService.DeleteTypeAsync(id);

            if (!deleted)
            {
                return BadRequest();
            }

            return Ok(deleted);
        }
    }
}
