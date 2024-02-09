using Microsoft.EntityFrameworkCore.Migrations.Operations;
using OgrenciBilgiSistemi.Domain.DataBaseContext;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;

namespace OgrenciBilgiSistemi.Domain.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(Context context) : base(context)
        {
        }
    }
}
