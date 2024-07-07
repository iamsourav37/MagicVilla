using MagicVilla.API.Models.DTO;

namespace MagicVilla.API.Repository
{
    public interface IVillaRepository
    {
        Task<IEnumerable<VillaDTO>> GetAll();
        Task<VillaDTO>? GetById(int id);
        Task<VillaDTO> CreateVilla(VillaCreateDTO villaCreateDTO);
        Task<bool> DeleteVilla(int id);   
        Task<VillaDTO> UpdateVilla(int id, VillaUpdateDTO villaUpdateDTO);
    }
}
