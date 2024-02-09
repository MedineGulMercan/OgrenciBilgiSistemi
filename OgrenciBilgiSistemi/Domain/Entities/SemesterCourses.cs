using OgrenciBilgiSistemi.Domain.Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciBilgiSistemi.Domain.Entities
{
    public class SemesterCourses : BaseEntity
    {
        [Required]
        [Column("semester_id")]
        [ForeignKey("semester_id")]
        public Guid SemesterId { get; set; }
        public virtual Semester Semester { get; set; }

        [Required]
        [Column("department_id")]
        [ForeignKey("department_id")]
        public Guid DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [Required]
        [Column("class_id")]
        [ForeignKey("class_id")]
        public Guid ClassId { get; set; } 
        public virtual Class Class { get; set; }

        [Required]
        [Column("course_id")]
        [ForeignKey("course_id")]
        public Guid CourseId { get; set; }
        public virtual Course Courses { get; set; }
        
        [Required]
        [Column("teacher_id")]
        [ForeignKey("teacher_id")]
        public Guid TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
        
      
    }
}
