using OgrenciBilgiSistemi.Domain.DataBaseContext;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;

namespace OgrenciBilgiSistemi.Domain.Repositories
{
    public class LetterGradeRepository : Repository<LetterGrade>, ILetterGradeRepository
    {
        public LetterGradeRepository(Context context) : base(context)
        {
        }
    }
}
