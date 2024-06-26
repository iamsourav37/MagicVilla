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
        [HttpGet]
        public IEnumerable<VillaDTO> GetVillas()
        {
            return new List<VillaDTO>()
            {
                new VillaDTO(){Id=1, Name="Panch Pakhori"},
                new VillaDTO(){Id=2, Name="Maa Homestay"},
                new VillaDTO(){Id=3, Name="Megh Bitan"},
            };
        }
    }
}
