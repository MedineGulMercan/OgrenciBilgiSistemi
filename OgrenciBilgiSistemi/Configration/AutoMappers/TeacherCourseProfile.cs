using AutoMapper;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Dto.User;

namespace OgrenciBilgiSistemi.Configration.AutoMappers
{
    public class TeacherCourseProfile : Profile
    {
        public TeacherCourseProfile()
        {
            CreateMap<TeacherCourseCreateDto, TeacherCourse>();
        }
    }
}
