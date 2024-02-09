using OgrenciBilgiSistemi.Domain.DataBaseContext;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;

namespace OgrenciBilgiSistemi.Domain.Repositories
{
    public class ExamGradeRepository : Repository<ExamGrade>, IExamGradeRepository
    {
        public ExamGradeRepository(Context context) : base(context)
        {
        }
    }
}
