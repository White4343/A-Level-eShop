namespace Catalog.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly ILogger<ItemController> _logger;

        public ItemController(IItemService itemService, ILogger<ItemController> logger)
        {
            _itemService = itemService;
            _logger = logger;
        }

        [HttpPost(Name = "CreateItem")]
        public async Task<ActionResult<Item>> Post([FromBody] Item item)
        {
            var createdItem = await _itemService.CreateItemAsync(item);

            if (createdItem == null)
            {
                return BadRequest();
            }

            return Ok(CreatedAtRoute("GetItemById", new { id = createdItem.Id }, createdItem));
        }

        [HttpGet(Name = "GetItems")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Item>>> Get()
        {
            var items = await _itemService.GetItemsAsync();

            if (items == null)
            {
                return NotFound();
            }

            return Ok(items);
        }

        [HttpGet("{id}", Name = "GetItemById")]
        [AllowAnonymous]
        public async Task<ActionResult<Item>> Get(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPut(Name = "UpdateItem")]
        public async Task<ActionResult<bool>> Put([FromBody] Item item)
        {
            var updated = await _itemService.UpdateItemAsync(item);

            if (updated == null)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPatch("{id}", Name = "PatchItemQuantity")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> Patch(int id, [FromQuery] int quantity)
        {
            var patched = await _itemService.PatchItemQuantityAsync(id, quantity);

            if (patched == null)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteItem")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var deleted = await _itemService.DeleteItemAsync(id);

            if (!deleted)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
