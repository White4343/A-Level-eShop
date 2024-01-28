using Type = Catalog.API.Data.Entities.Type;

namespace Catalog.API.Services
{
    public class TypeService : ITypeService 
    {
        private readonly ITypeRepository _typeRepository;
        private readonly ILogger<TypeService> _logger;

        public TypeService(ITypeRepository typeRepository, ILogger<TypeService> logger)
        {
            _typeRepository = typeRepository;
            _logger = logger;
        }

        public async Task<Type> CreateTypeAsync(Type type)
        {
            return await _typeRepository.CreateTypeAsync(type);
        }

        public async Task<IEnumerable<Type>> GetTypesAsync()
        {
            return await _typeRepository.GetTypesAsync();
        }

        public async Task<Type> GetTypeByIdAsync(int id)
        {
            return await _typeRepository.GetTypeByIdAsync(id);
        }

        public async Task<Type> UpdateTypeAsync(Type type)
        {
            return await _typeRepository.UpdateTypeAsync(type);
        }

        public async Task<bool> DeleteTypeAsync(int id)
        {
            return await _typeRepository.DeleteTypeAsync(id);
        }
    }
}
