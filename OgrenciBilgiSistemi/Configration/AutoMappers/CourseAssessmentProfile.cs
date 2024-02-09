using AutoMapper;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Dto.CourseAssessment;

namespace OgrenciBilgiSistemi.Configration.AutoMappers
{
    public class CourseAssessmentProfile : Profile
    {
        public CourseAssessmentProfile()
        {
            CreateMap<CourseAssessmentCreateDto, CourseAssessment>();
            CreateMap<CourseAssessmentUpdateDto, CourseAssessment>();
        }
    }
}
