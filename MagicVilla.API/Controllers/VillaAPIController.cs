using AutoMapper;
using MagicVilla.API.Models;
using MagicVilla.API.Models.DTO;
using MagicVilla.API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography.Xml;

namespace MagicVilla.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly IVillaRepository _villaRepository;
        private readonly IMapper _mapper;
        private ApiResponse _apiResponse;

        public VillaAPIController(IVillaRepository villaRepository, IMapper mapper)
        {

            this._villaRepository = villaRepository;
            this._mapper = mapper;
            this._apiResponse = new ApiResponse();
        }


        [HttpGet]
        public async Task<IActionResult> GetVillas()
        {
            var villaDto = await this._villaRepository.GetAllAsync();
            this._apiResponse.StatusCode = HttpStatusCode.OK;
            this._apiResponse.Result = villaDto;
            return Ok(_apiResponse);
        }

        [HttpGet("{id}", Name = "GetVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetVilla(int id)
        {
            var result = await this._villaRepository.GetAsync(e => e.Id == id);
            if (result is null)
            {
                this._apiResponse.StatusCode = HttpStatusCode.NotFound;
                this._apiResponse.IsSuccess = false;
                this._apiResponse.Errors.Add(new string("Villa not found"));
                return NotFound(_apiResponse);
            }
            this._apiResponse.StatusCode = HttpStatusCode.OK;
            this._apiResponse.Result = result;
            return Ok(_apiResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVilla([FromBody] VillaCreateDTO villaCreateDTO)
        {

            if (villaCreateDTO is null)
            {
                this._apiResponse.StatusCode = HttpStatusCode.BadRequest;
                this._apiResponse.IsSuccess = false;
                return BadRequest(_apiResponse);
            }

            var villaModel = this._mapper.Map<Villa>(villaCreateDTO);
            var result = await this._villaRepository.CreateAsync(villaModel);
            this._apiResponse.StatusCode = HttpStatusCode.Created;
            this._apiResponse.Result = result;
            return CreatedAtAction(nameof(GetVilla), new { id = result.Id }, _apiResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            var existingVilla = await this._villaRepository.GetAsync(e => e.Id == id);
            var flag = await this._villaRepository.RemoveAsync(existingVilla);
            if (!flag)
            {
                this._apiResponse.StatusCode = HttpStatusCode.BadRequest;
                this._apiResponse.IsSuccess = false;
                return BadRequest(_apiResponse);
            }
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVilla([FromBody] VillaUpdateDTO villaUpdateDTO)
        {
            var villaDto = await this._villaRepository.UpdateAsync(villaUpdateDTO);
            this._apiResponse.StatusCode = HttpStatusCode.OK;
            this._apiResponse.Result = villaDto;
            return Ok(_apiResponse);
        }
    }
}
