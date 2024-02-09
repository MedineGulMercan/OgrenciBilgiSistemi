using OgrenciBilgiSistemi.Domain.DataBaseContext;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;

namespace OgrenciBilgiSistemi.Domain.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(Context context) : base(context)
        {
        }
    }
}
