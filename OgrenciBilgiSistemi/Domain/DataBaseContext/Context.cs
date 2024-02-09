using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemi.Domain.Entities;
using System.Reflection.Emit;

namespace OgrenciBilgiSistemi.Domain.DataBaseContext
{
    public class Context : DbContext
    {
        public Context()
        {
        }
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseAssessment> CourseAssessments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentLanguage> DepartmentLanguages { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<SemesterCourses> SemesterCoursess { get; set; }
        public DbSet<StudentAssessmentGrade> StudentAssessmentGrades { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherCourse> TeacherCourses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AssessmentType> AssessmentTypes { get; set; }
        public DbSet<CurrentSemester> CurrentSemesters { get; set; }
        public DbSet<CourseSelection> CourseSelections { get; set; }
        public DbSet<ExamGrade> ExamGrades { get; set; }
        public DbSet<CourseLetterScore> CourseLetterScores { get; set; }
        public DbSet<LetterGrade> LetterGrades { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Diğer ilişkileri de benzer şekilde tanımlayın
            base.OnModelCreating(builder);
        }
    }
}
