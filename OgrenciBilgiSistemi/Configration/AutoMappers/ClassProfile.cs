using AutoMapper;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Dto.Class;

namespace OgrenciBilgiSistemi.Configration.AutoMappers
{
    public class ClassProfile : Profile
    {
        public ClassProfile()
        {
            CreateMap<ClassCreateDto, Class>();
            CreateMap<ClassUpdateDto, Class>();
        }
    }
}
