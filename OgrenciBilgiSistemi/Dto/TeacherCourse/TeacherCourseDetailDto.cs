using OgrenciBilgiSistemi.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciBilgiSistemi.Dto.TeacherCourse
{
    public class TeacherCourseDetailDto
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public string TeacherName { get; set; }
        public Guid TeacherId { get; set; }
    }
}
