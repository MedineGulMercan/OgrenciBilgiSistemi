using AutoMapper;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Dto.CourseLetterScore;

namespace OgrenciBilgiSistemi.Configration.AutoMappers
{
    public class CourseLetterScoreProfile : Profile
    {
        public CourseLetterScoreProfile()
        {
            CreateMap<CourseLetterScoreUpdateDto, CourseLetterScore>();
        }
    }
}
