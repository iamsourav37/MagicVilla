using AutoMapper;
using MagicVilla.API.Data;
using MagicVilla.API.Models;
using MagicVilla.API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.API.Repository
{
    public class VillaRepository : IVillaRepository
    {
        private readonly ILogger<VillaRepository> _logger;
        private readonly VillaDBContext _db;
        private readonly IMapper _mapper;

        public VillaRepository(ILogger<VillaRepository> logger, VillaDBContext db, IMapper mapper)
        {
            this._logger = logger;
            this._db = db;
            this._mapper = mapper;
        }


        public async Task<VillaDTO> CreateVilla(VillaCreateDTO villaCreateDTO)
        {

            var model = _mapper.Map<Villa>(villaCreateDTO);

            await this._db.Villas.AddAsync(model);
            await this._db.SaveChangesAsync();
            var villaDto = this._mapper.Map<VillaDTO>(model);
            return villaDto;
        }

        public async Task<bool> DeleteVilla(int id)
        {
            var existingVilla = await this._db.Villas.FindAsync(id);

            if (existingVilla is not null)
            {
                this._db.Villas.Remove(existingVilla);
                await this._db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<VillaDTO>> GetAll()
        {

            this._logger.LogInformation("GetVillas() invoked.");
            var villas = await this._db.Villas.ToListAsync();
            var villaDto = this._mapper.Map<IEnumerable<VillaDTO>>(villas);
            return villaDto;
        }

        public async Task<VillaDTO>? GetById(int id)
        {
            this._logger.LogInformation($"GetVilla() invoked, with id: {id}");

            var result = this._db.Villas.Where(e => e.Id == id).FirstOrDefault();
            if (result is null)
            {
                return null;
            }
            var villaDto = _mapper.Map<VillaDTO>(result);
            return villaDto;
        }

        public async Task<VillaDTO> UpdateVilla(int id, VillaUpdateDTO villaUpdateDTO)
        {
            var existingVilla = await this._db.Villas.FindAsync(id);
            if (existingVilla is not null)
            {
                this._mapper.Map(villaUpdateDTO, existingVilla);
                // Save changes to the database
                this._db.Villas.Update(existingVilla);
                await this._db.SaveChangesAsync();

                var villaDto = _mapper.Map<VillaDTO>(existingVilla);
                return villaDto;
            }
            return null;
        }
    }
}
