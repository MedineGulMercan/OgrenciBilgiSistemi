using Microsoft.Build.Framework;
using OgrenciBilgiSistemi.Domain.Entities.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciBilgiSistemi.Domain.Entities
{
    public class CourseSelection : BaseEntity
    {
        [Required]
        [Column("student_id")]
        public Guid StudentId { get; set; }

        [Required]
        [Column("class_id")]
        public Guid ClassId { get; set; }

        [Required]
        [Column("semester_courses_id")]
        [ForeignKey("SemesterCoursesId")]
        public Guid SemesterCoursesId { get; set; }
        public virtual SemesterCourses SemesterCourses { get; set; } 

        [Required]
        [Column("is_registiration_confirmed")]
        public bool IsRegistrationConfirmed { get; set; }

    }
}
