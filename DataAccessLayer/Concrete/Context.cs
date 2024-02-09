using EntityLayer;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO : (MEDİNE) Kod içerisinde sql connection yolu verilmez. Jsonda tanımlanıp çekilir.
            optionsBuilder.UseSqlServer("Server=DESKTOP-F7K2U1C\\SQLEXPRESS;database=OgrenciBilgiSistemi;Integrated Security = true; TrustServerCertificate=True;");
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseAssessment> CourseAssessments { get; set; }
        public DbSet<DepartmentCourse> DepartmentCourses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<StudentAssessmentGrade> StudentAssessmentGrades { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<TeacherDepartment> TeacherDepartments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
