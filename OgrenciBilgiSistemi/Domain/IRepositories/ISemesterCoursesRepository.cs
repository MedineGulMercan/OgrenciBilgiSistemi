using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Dto.SemesterCourses;

namespace OgrenciBilgiSistemi.Domain.IRepositories
{
    public interface ISemesterCoursesRepository : IRepository<SemesterCourses>
    {
        IQueryable<SemesterCoursesTableDto> GetAllInnerJoin();
    }
}
