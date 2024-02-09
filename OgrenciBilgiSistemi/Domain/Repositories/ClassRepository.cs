using OgrenciBilgiSistemi.Domain.DataBaseContext;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;
namespace OgrenciBilgiSistemi.Domain.Repositories
{

    public class ClassRepository : Repository<Class>, IClassRepository
    {
        public ClassRepository(Context context) : base(context)
        {
        }
    }
}
