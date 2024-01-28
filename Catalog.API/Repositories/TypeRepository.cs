using Type = Catalog.API.Data.Entities.Type;

namespace Catalog.API.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TypeRepository> _logger;

        public TypeRepository(AppDbContext context, ILogger<TypeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Type> CreateTypeAsync(Type type)
        {
            if (type == null)
            {
                _logger.LogError("Type object sent from client is null.");

                return null;
            }

            _context.Types.Add(type);

            await _context.SaveChangesAsync();

            return type;
        }

        public async Task<IEnumerable<Type>> GetTypesAsync()
        {
            var types = await _context.Types.ToListAsync();

            if (types == null)
            {
                _logger.LogError("Types not found.");

                return null;
            }

            return types;
        }

        public async Task<Type> GetTypeByIdAsync(int id)
        {
            var type = await _context.Types.FindAsync(id);

            if (type == null)
            {
                _logger.LogError($"Type with id: {id}, not found.");

                return null;
            }

            return type;
        }

        public async Task<Type> UpdateTypeAsync(Type type)
        {
            if (type == null)
            {
                _logger.LogError("Type object sent from client is null.");

                return null;
            }

            var typeToUpdate = await _context.Types.FindAsync(type.Id);

            if (typeToUpdate == null)
            {
                _logger.LogError($"Type with id: {type.Id}, not found.");

                return null;
            }

            _context.Types.Update(type);

            await _context.SaveChangesAsync();

            return type;
        }

        public async Task<bool> DeleteTypeAsync(int id)
        {
            var typeToDelete = await _context.Types.FindAsync(id);

            if (typeToDelete == null)
            {
                _logger.LogError($"Type with id: {id}, not found.");

                return false;
            }

            _context.Types.Remove(typeToDelete);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
