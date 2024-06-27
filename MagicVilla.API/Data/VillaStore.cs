using MagicVilla.API.Models.DTO;
using System.Xml.Linq;

namespace MagicVilla.API.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> VillaList = new List<VillaDTO>()
            {
                new VillaDTO() { Id = 1, Name = "Panch Pakhori"},
                new VillaDTO() { Id = 2, Name = "Maa Homestay"},
                new VillaDTO() { Id = 3, Name = "Megh Bitan"},
            };
    }
}
