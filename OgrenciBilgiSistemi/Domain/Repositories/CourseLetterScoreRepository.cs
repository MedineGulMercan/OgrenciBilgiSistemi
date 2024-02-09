using OgrenciBilgiSistemi.Domain.DataBaseContext;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;

namespace OgrenciBilgiSistemi.Domain.Repositories
{
    public class CourseLetterScoreRepository : Repository<CourseLetterScore>, ICourseLetterScoreRepository
    {
        public CourseLetterScoreRepository(Context context) : base(context)
        {
        }
    }
}
