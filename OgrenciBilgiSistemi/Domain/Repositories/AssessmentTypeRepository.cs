using OgrenciBilgiSistemi.Domain.DataBaseContext;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;

namespace OgrenciBilgiSistemi.Domain.Repositories
{
    public class AssessmentTypeRepository : Repository<AssessmentType>, IAssessmentTypeRepository
    {
        public AssessmentTypeRepository(Context context) : base(context)
        {
        }
    }
}
