using MagicVilla.API.Models;

namespace MagicVilla.API.Repository
{
    public interface IVillaNumberRepository
    {
        Task<IEnumerable<VillaNumber>> GetAllAsync();
        Task<VillaNumber> GetByIdAsync(int id);
        Task CreateAsync(VillaNumber villaNumber);
        Task UpdateAsync(VillaNumber villaNumber);
        Task DeleteAsync(VillaNumber villaNumber);
    }
}
