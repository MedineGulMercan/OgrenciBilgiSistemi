using OgrenciBilgiSistemi.Domain.Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace OgrenciBilgiSistemi.Domain.Entities
{
    public class CourseAssessment : BaseEntity 
    {
        [Required]
        [Column("assesment_name", TypeName = "nvarchar(max)")]
        public string? AssessmentName { get; set; }
        [Required]
        [Column("passing_score", TypeName = "int")]
        public int PassingScore { get; set; }

        [Required]
        [Column("impact_course_grade", TypeName = "int")]
        public int ImpactCourseGrade { get; set; }

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

        [Required]
        [Column("assessment_type_id")]
        [ForeignKey("assessment_type_id")]
        public Guid AssessmentTypeId { get; set; }
        public virtual AssessmentType AssessmentType { get; set; }

        [Required]
        [Column("current_semesters_id")]
        public Guid CurrentSemestersId { get; set; }
    }
}
