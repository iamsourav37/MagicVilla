using MagicVilla.API.Models;
using MagicVilla.API.Models.DTO;

namespace MagicVilla.API.Repository
{
    public interface IVillaRepository : IRepository<Villa>
    {
        Task<VillaDTO> UpdateAsync(VillaUpdateDTO villaUpdateDTO);
    }
}
