using System.Net;
using Catalog.API.Models;

namespace Catalog.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BffItemController : ControllerBase
    {
        private readonly IBffItemService _bffItemService;
        private readonly ILogger<BffItemController> _logger;

        public BffItemController(IBffItemService bffItemService, ILogger<BffItemController> logger)
        {
            _bffItemService = bffItemService;
            _logger = logger;
        }

        [HttpPost(Name = "GetItemsByPage")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PaginatedItemsResponse<ItemDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Items(PaginatedItemsRequest<BffItemFilter> request)
        {
            var result = await _bffItemService.GetByPageAsync(request.PageIndex, request.PageSize, request.Filters);
            return Ok(result);
        }
    }
}
