using AutoMapper;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Dto.AssessmentType;

namespace OgrenciBilgiSistemi.Configration.AutoMappers
{
    public class AssessmentTypeProfile : Profile
    {
        public AssessmentTypeProfile() {

            CreateMap<AssessmentTypeCreateDto, AssessmentType>();
            CreateMap<AssessmentTypeUpdateDto, AssessmentType>();
        }
    }
}
