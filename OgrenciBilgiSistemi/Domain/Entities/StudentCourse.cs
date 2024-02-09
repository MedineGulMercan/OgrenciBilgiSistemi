using OgrenciBilgiSistemi.Domain.Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciBilgiSistemi.Domain.Entities
{
    public class StudentCourse : BaseEntity 
    {
        [Required]
        [Column("course_id")]
        [ForeignKey("course_id")]
        public Guid CourseId { get; set; }
        public virtual Course Courses { get; set; }

        [Required]
        [Column("student_id")]
        [ForeignKey("student_id")]
        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}
