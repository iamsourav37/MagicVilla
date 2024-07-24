using AutoMapper;
using MagicVilla.API.Data;
using MagicVilla.API.Models;
using MagicVilla.API.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla.API.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ILogger<VillaRepository> _logger;
        private readonly VillaDBContext _db;
        private readonly IMapper _mapper;

        public VillaRepository(ILogger<VillaRepository> logger, VillaDBContext db, IMapper mapper) : base(db)
        {
            this._logger = logger;
            this._db = db;
            this._mapper = mapper;
        }

       
        public async Task<VillaDTO> CreateVilla(VillaCreateDTO villaCreateDTO)
        {
            var villaModel = _mapper.Map<Villa>(villaCreateDTO);

            var result = await this.CreateAsync(villaModel);
            var villaDto = this._mapper.Map<VillaDTO>(result);
            return villaDto;
        }

        public async Task<IEnumerable<VillaDTO>> GetAll()
        {

            this._logger.LogInformation("GetVillas() invoked.");
            var villas = await this._db.Villas.ToListAsync();
            var villaDto = this._mapper.Map<IEnumerable<VillaDTO>>(villas);
            return villaDto;
        }       
        public async Task<VillaDTO> UpdateAsync(VillaUpdateDTO villaUpdateDTO)
        {
            var existingVilla = await this._db.Villas.FindAsync(villaUpdateDTO.Id);
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
