using AutoMapper;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Models;

namespace SwimmingAcademy.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Info2, SwimmerDto>().ReverseMap();
        }
    }
}
