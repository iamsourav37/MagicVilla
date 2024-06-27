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

        public VillaAPIController(ILogger<VillaAPIController> logger, VillaDBContext db)
        {
            this._logger = logger;
            this._db = db;
        }


        [HttpGet]
        public IActionResult GetVillas()
        {
            this._logger.LogInformation("GetVillas() invoked.");
            return Ok(this._db.Villas.ToList());
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
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateVilla([FromBody] VillaDTO villaDTO)
        {
            this._logger.LogInformation($"CreateVilla() invoked, with villaDto id: {villaDTO.Id}");

            if (villaDTO is null)
            {
                this._logger.LogError("villaDto is null");
                return BadRequest(villaDTO);
            }
            if (villaDTO.Id > 0)
            {
                this._logger.LogError("the id is provided is greater than 0.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var result = this._db.Villas.Add(new Villa() { Name = villaDTO.Name, Amenity = villaDTO.Amenity, Details = villaDTO.Details, ImageUrl = villaDTO.ImageUrl, Occupancy = villaDTO.Occupancy, Rate = villaDTO.Rate, Sqft = villaDTO.Sqft, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
            this._db.SaveChanges();
            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        }
    }
}
