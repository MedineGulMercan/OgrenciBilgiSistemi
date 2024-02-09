using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Dto.TeacherCourse;

namespace OgrenciBilgiSistemi.Domain.IRepositories
{
    public interface ITeacherCourseRepository : IRepository<TeacherCourse>
    {
        Task<List<TeacherCourseDetailDto>> GetTeacherCourseInnerJoinByCourseId(Guid? id);
    }
}
