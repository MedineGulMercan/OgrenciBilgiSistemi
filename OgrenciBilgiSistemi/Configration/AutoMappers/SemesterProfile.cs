using AutoMapper;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Dto.Semester;

namespace OgrenciBilgiSistemi.Configration.AutoMappers
{
    public class SemesterProfile : Profile
    {
        public SemesterProfile()
        {
            CreateMap<SemesterCreateDto, Semester>();
            CreateMap<SemesterUpdateDto, Semester>();
        }
    }
}
