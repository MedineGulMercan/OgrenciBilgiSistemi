using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemi.Domain.DataBaseContext;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;
using OgrenciBilgiSistemi.Dto.CourseSelection;
using OgrenciBilgiSistemi.Dto.SemesterCourses;
using System.Linq;

namespace OgrenciBilgiSistemi.Domain.Repositories
{
    public class CourseSelectionRepository : Repository<CourseSelection>, ICourseSelectionRepository
    {

        private readonly Context _context;
        public CourseSelectionRepository(Context context) : base(context)
        {
            _context = context;
        }
        public IQueryable<CourseSelectionDto> GetAllInnerJoin(Guid id, Guid classId, Guid semesterId)
        {
            // burda öğrencinin id 'sine göre ve bulunduğu sınıf ve semestırına göre seçtiği dersleri getiriyorum
            var result = (from cs in _context.CurrentSemesters.Where(s => s.IsActive )
                          join s in _context.Semesters on cs.SemesterId equals s.Id
                          join cosl in _context.CourseSelections.Include(x => x.SemesterCourses).Where(x=>x.ClassId == classId && x.SemesterCourses.SemesterId == semesterId) 
                          on id equals cosl.StudentId
                          join sc in _context.SemesterCoursess on cosl.SemesterCoursesId equals sc.Id
                          join t in _context.Teachers on sc.TeacherId equals t.Id
                          join c in _context.Courses on sc.CourseId equals c.Id 
                          select new CourseSelectionDto
                          {
                              CourseId = c.Id,
                              CourseName = c.CourseName,
                              TeacherId = t.Id,
                              TeacherNameSurname = t.Name + t.Surname,
                              CurrentSemesterId = s.Id,
                              ClassId = cosl.SemesterCourses.ClassId,
                              CourseCredit = c.CourseCredit
                          });
            return result;
        }
        public IQueryable<Course> GetAllTeacherCourse(Guid teacherId)
        {
            var query = (from tc in _context.TeacherCourses.Where(x => x.TeacherId == teacherId).Include(x => x.Course)
                         join cs in _context.CourseSelections.Where(x=>x.IsRegistrationConfirmed).Include(x => x.SemesterCourses) on tc.CourseId equals cs.SemesterCourses.CourseId
                         select tc.Course).Distinct();
            return query;
        }
    }
}
