using OgrenciBilgiSistemi.Domain.Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciBilgiSistemi.Domain.Entities
{
    public class TeacherCourse : BaseEntity
    {
        [Required]
        [Column("course_id")]
        [ForeignKey("course_id")]
        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; }

        [Required]
        [Column("teacher_id")]
        [ForeignKey("teacher_id")]
        public Guid TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
