using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemi.Domain.DataBaseContext;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;
using OgrenciBilgiSistemi.Dto.TeacherCourse;
namespace OgrenciBilgiSistemi.Domain.Repositories
{
    public class TeacherCourseRepository : Repository<TeacherCourse>, ITeacherCourseRepository
    {
        private readonly Context _context;
        public TeacherCourseRepository(Context context) : base(context)
        {
            _context = context;
        }
        public async Task<List<TeacherCourseDetailDto>> GetTeacherCourseInnerJoinByCourseId(Guid? id)
        {
            if (id is not null)
            {
                // burda teachera bağlı dersleri çekmişim şimdi tam tesri
                var data = await (from c in _context.Courses where c.Id == id
                                  join tc in _context.TeacherCourses on c.Id equals tc.CourseId
                                  join t in _context.Teachers on tc.TeacherId equals t.Id
                                  select new TeacherCourseDetailDto
                                  {
                                      TeacherId = t.Id,
                                      CourseId = c.Id,
                                      CourseName = c.CourseName,
                                      TeacherName=t.Name,
                                  }).ToListAsync();
                return data;
            }
            return new List<TeacherCourseDetailDto>();
        }
    }
}
