namespace Catalog.API.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ItemRepository> _logger;

        public ItemRepository(AppDbContext context, ILogger<ItemRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Item> CreateItemAsync(Item item)
        {
            if (item == null)
            {
                _logger.LogError("Item object sent from client is null.");

                return null;
            }

            _context.Items.Add(item);

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            var items = await _context.Items.ToListAsync();

            if (items == null)
            {
                _logger.LogError("Items not found.");

                return null;
            }

            return items;
        }

        public async Task<Item> GetItemByIdAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                _logger.LogError($"Item with id: {id}, not found.");

                return null;
            }

            return item;
        }

        public async Task<Item> UpdateItemAsync(Item item)
        {
            if (item == null)
            {
                _logger.LogError("Item object sent from client is null.");

                return null;
            }

            var itemToUpdate = await _context.Items.FindAsync(item.Id);

            if (itemToUpdate == null)
            {
                _logger.LogError($"Item with id: {item.Id}, not found.");

                return null;
            }

            _context.Items.Update(item);

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<Item> PatchItemQuantityAsync(int id, int quantity)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                _logger.LogError($"Item with id: {id}, not found.");

                return null;
            }

            item.AvailableStock -= quantity;

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var itemToDelete = await _context.Items.FindAsync(id);

            if (itemToDelete == null)
            {
                _logger.LogError($"Item with id: {id}, not found.");

                return false;
            }

            _context.Items.Remove(itemToDelete);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
