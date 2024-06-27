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

        public VillaAPIController(ILogger<VillaAPIController> logger)
        {
            this._logger = logger;
        }


        [HttpGet]
        public IActionResult GetVillas()
        {
            this._logger.LogInformation("GetVillas() invoked.");
            return Ok(VillaStore.VillaList);
        }

        [HttpGet("{id}", Name ="GetVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetVilla(int id)
        {
            this._logger.LogInformation($"GetVilla() invoked, with id: {id}");

            var result = VillaStore.VillaList.Where(e => e.Id == id).FirstOrDefault();
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
            villaDTO.Id = VillaStore.VillaList.OrderByDescending(e => e.Id).First().Id + 1;
            VillaStore.VillaList.Add(new VillaDTO() { Id = villaDTO.Id, Name = villaDTO.Name });
            return CreatedAtRoute("GetVilla", new { id=villaDTO.Id }, villaDTO);
        }
    }
}
