using OgrenciBilgiSistemi.Domain.DataBaseContext;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;
using OgrenciBilgiSistemi.Dto.SemesterCourses;

namespace OgrenciBilgiSistemi.Domain.Repositories
{
    public class SemesterCoursesRepository : Repository<SemesterCourses>, ISemesterCoursesRepository
    {
        private readonly Context _context;
        public SemesterCoursesRepository(Context context) : base(context)
        {
            _context = context;
        }
        public IQueryable<SemesterCoursesTableDto> GetAllInnerJoin()
        {
            var result = (from sc in _context.SemesterCoursess
                          join s in _context.Semesters on sc.SemesterId equals s.Id
                          join d in _context.Departments on sc.DepartmentId equals d.Id
                          join c in _context.Classes on sc.ClassId equals c.Id
                          join course in _context.Courses on sc.CourseId equals course.Id
                          join t in _context.Teachers on sc.TeacherId equals t.Id
                          select new SemesterCoursesTableDto
                          {
                              ClassId = sc.ClassId,
                              ClassName = c.ClassName,
                              SemesterId = sc.SemesterId,
                              SemesterName = s.SemesterName,
                              CourseId = sc.CourseId,
                              CourseName = course.CourseName,
                              DepartmentId = sc.DepartmentId,
                              DepartmentName = d.DepartmentName,
                              TeacherId= sc.TeacherId,
                              TeacherName=t.Name + " " +t.Surname,

                          });
            return result;
        }
    }
}
