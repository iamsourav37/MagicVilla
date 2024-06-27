using AutoMapper;
using MagicVilla.API.Models;
using MagicVilla.API.Models.DTO;

namespace MagicVilla.API
{
    public class MappingConfig : Profile
    {

        public MappingConfig()
        {
            CreateMap<Villa, VillaCreateDTO>();
            CreateMap<VillaCreateDTO, Villa>();

            CreateMap<Villa, VillaDTO>();
            CreateMap<VillaDTO, Villa>();

            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();
        }
    }
}
