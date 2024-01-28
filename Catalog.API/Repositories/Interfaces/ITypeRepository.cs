using Type = Catalog.API.Data.Entities.Type;

namespace Catalog.API.Repositories.Interfaces
{
    public interface ITypeRepository
    {
        Task<Type> CreateTypeAsync(Type type);

        Task<IEnumerable<Type>> GetTypesAsync();

        Task<Type> GetTypeByIdAsync(int id);

        Task<Type> UpdateTypeAsync(Type type);

        Task<bool> DeleteTypeAsync(int id);
    }
}
