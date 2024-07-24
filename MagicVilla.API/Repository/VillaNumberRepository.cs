using AutoMapper;
using MagicVilla.API.Data;
using MagicVilla.API.Models;

namespace MagicVilla.API.Repository
{
    public class VillaNumberRepository : IVillaNumberRepository
    {

        private readonly ILogger<VillaRepository> _logger;
        private readonly VillaDBContext _db;
        private readonly IMapper _mapper;

        public VillaNumberRepository(ILogger<VillaRepository> logger, VillaDBContext db, IMapper mapper)
        {
            this._logger = logger;
            this._db = db;
            this._mapper = mapper;
        }

        public Task CreateAsync(VillaNumber villaNumber)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(VillaNumber villaNumber)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VillaNumber>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<VillaNumber> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(VillaNumber villaNumber)
        {
            throw new NotImplementedException();
        }
    }
}
