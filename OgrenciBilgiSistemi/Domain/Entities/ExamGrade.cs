using OgrenciBilgiSistemi.Domain.Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciBilgiSistemi.Domain.Entities
{
    public class ExamGrade : BaseEntity
    {
        [Required]
        [Column("course_assessment_id")]
        [ForeignKey("course_assessment_id")]
        public Guid CourseAssessmentId { get; set; }
        public virtual CourseAssessment CourseAssessment { get; set; }

        [Required]
        [Column("student_id")]
        public Guid StudentId { get; set; }

        [Required]
        [Column("score")]
        public int Score { get; set; }
    }
}
