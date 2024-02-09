using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Dto.User;

namespace OgrenciBilgiSistemi.Domain.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<UserDetailDto> GetUserDetail(Guid id);
    }
}
