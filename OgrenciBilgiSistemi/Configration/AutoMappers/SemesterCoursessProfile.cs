using AutoMapper;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Dto.SemesterCourses;

namespace OgrenciBilgiSistemi.Configration.AutoMappers
{
    public class SemesterCoursessProfile : Profile
    {
        public SemesterCoursessProfile()
        {
            CreateMap<SemesterCoursesUpdateDto, SemesterCourses>();
        }
    }
}
