using AutoMapper;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Dto.DepartmentLanguage;

namespace OgrenciBilgiSistemi.Configration.AutoMappers
{
    public class DepartmentLanguageProfile : Profile
    {
        public DepartmentLanguageProfile()
        {
            CreateMap<DepartmentLanguageCreateDto, DepartmentLanguage>();
            CreateMap<DepartmentLanguageUpdateDto, DepartmentLanguage>(); 
        }
    }
}
