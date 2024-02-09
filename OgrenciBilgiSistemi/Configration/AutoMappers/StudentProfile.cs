using AutoMapper;
using OgrenciBilgiSistemi.Areas.Admin.Models.User;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Dto.User;

namespace OgrenciBilgiSistemi.Configration.AutoMappers
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<UserCreateVM, Student>()
              .ForMember(dest => dest.User, opt => opt.MapFrom(src => new User
              {
                  RoleId = src.RoleId,
                  Password = src.TC
              }));
            CreateMap<UserUpdateDto, Student>()
              .ForMember(dest => dest.User, opt => opt.MapFrom(src => new User
              {
                  Id = src.UserId,
                  RoleId = src.RoleId,
                  Password = src.TC
              }));
        }
    }
}
