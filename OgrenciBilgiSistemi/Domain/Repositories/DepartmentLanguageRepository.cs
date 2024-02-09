using OgrenciBilgiSistemi.Domain.DataBaseContext;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;
namespace OgrenciBilgiSistemi.Domain.Repositories
{
    public class DepartmentLanguageRepository : Repository<DepartmentLanguage>, IDepartmentLanguageRepository
    {
        public DepartmentLanguageRepository(Context context) : base(context)
        {
        }
    }
}
