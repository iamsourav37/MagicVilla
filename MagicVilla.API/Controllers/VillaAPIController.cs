using AutoMapper;
using MagicVilla.API.Data;
using MagicVilla.API.Models;
using MagicVilla.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace MagicVilla.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogger<VillaAPIController> _logger;
        private readonly VillaDBContext _db;
        private readonly IMapper _mapper;

        public VillaAPIController(ILogger<VillaAPIController> logger, VillaDBContext db, IMapper mapper)
        {
            this._logger = logger;
            this._db = db;
            this._mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetVillas()
        {
            this._logger.LogInformation("GetVillas() invoked.");
            var villas = this._db.Villas.ToList();
            var villaDto = this._mapper.Map<IEnumerable<VillaDTO>>(villas);
            return Ok(villaDto);
        }

        [HttpGet("{id}", Name = "GetVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetVilla(int id)
        {
            this._logger.LogInformation($"GetVilla() invoked, with id: {id}");

            var result = this._db.Villas.Where(e => e.Id == id).FirstOrDefault();
            if (result is null)
            {
                var errorResponse = new
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Error = "Villa not found",
                    VillaId = id
                };
                return NotFound(errorResponse);
            }
            var villaDto = _mapper.Map<VillaDTO>(result);
            return Ok(villaDto);
        }

        [HttpPost]
        public IActionResult CreateVilla([FromBody] VillaCreateDTO villaCreateDTO)
        {

            if (villaCreateDTO is null)
            {
                this._logger.LogError("villaCreateDTO is null");
                return BadRequest(villaCreateDTO);
            }


            //var model = new Villa() { Name = villaCreateDTO.Name, Amenity = villaCreateDTO.Amenity, Details = villaCreateDTO.Details, ImageUrl = villaCreateDTO.ImageUrl, Occupancy = villaCreateDTO.Occupancy, Rate = villaCreateDTO.Rate, Sqft = villaCreateDTO.Sqft, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now };


            var model = _mapper.Map<Villa>(villaCreateDTO);

            this._db.Villas.Add(model);
            this._db.SaveChanges();
            return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
        }

        [HttpDelete]
        public IActionResult DeleteVilla(int id)
        {
            var existingVilla = this._db.Villas.Find(id);
            if (existingVilla == null)
            {
                return NotFound();
            }

            this._db.Villas.Remove(existingVilla);
            this._db.SaveChanges();
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateVilla(int id, [FromBody] VillaUpdateDTO villaUpdateDTO)
        {
            var existingVilla = this._db.Villas.Find(id);
            if (existingVilla == null)
            {
                return NotFound();
            }

            this._mapper.Map(villaUpdateDTO, existingVilla);

            // Update the timestamp
            existingVilla.UpdatedDate = DateTime.Now;

            // Update the existing villa's properties
            //existingVilla.Name = villaUpdateDTO.Name;
            //existingVilla.Amenity = villaUpdateDTO.Amenity;
            //existingVilla.Details = villaUpdateDTO.Details;
            //existingVilla.ImageUrl = villaUpdateDTO.ImageUrl;
            //existingVilla.Occupancy = villaUpdateDTO.Occupancy;
            //existingVilla.Rate = villaUpdateDTO.Rate;
            //existingVilla.Sqft = villaUpdateDTO.Sqft;
            //existingVilla.UpdatedDate = DateTime.Now;

            // Save changes to the database
            this._db.Villas.Update(existingVilla);
            this._db.SaveChanges();

            var villaDto = _mapper.Map<VillaDTO>(existingVilla);
            return Ok(villaDto);
        }
    }
}
