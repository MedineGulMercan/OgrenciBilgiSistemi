using AutoMapper;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Dto.CourseSelection;

namespace OgrenciBilgiSistemi.Configration.AutoMappers
{
    public class CourseSelectionProfile : Profile
    {
        public CourseSelectionProfile()
        {
            CreateMap<CourseSelectionsCreateDto, CourseSelection>(); 
        }
    }
}
