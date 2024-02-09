using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.Repositories;
using OgrenciBilgiSistemi.Dto.CourseSelection;

namespace OgrenciBilgiSistemi.Domain.IRepositories
{
    public interface ICourseSelectionRepository : IRepository<CourseSelection>
    {
        IQueryable<CourseSelectionDto> GetAllInnerJoin(Guid id, Guid classId, Guid semesterId);
        IQueryable<Course> GetAllTeacherCourse(Guid teacherId);
    }
}
