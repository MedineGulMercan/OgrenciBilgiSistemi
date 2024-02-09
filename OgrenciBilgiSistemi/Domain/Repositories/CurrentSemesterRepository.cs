using OgrenciBilgiSistemi.Domain.DataBaseContext;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;

namespace OgrenciBilgiSistemi.Domain.Repositories
{
    public class CurrentSemesterRepository : Repository<CurrentSemester>, ICurrentSemesterRepository
    {
        public CurrentSemesterRepository(Context context) : base(context)
        {
        }
    }
}
