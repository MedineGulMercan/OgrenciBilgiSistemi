using OgrenciBilgiSistemi.Domain.Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciBilgiSistemi.Domain.Entities
{
    public class CourseLetterScore : BaseEntity
    {
        [Required]
        [Column("course_id")]
        [ForeignKey("course_id")]
        public Guid CourseId { get; set; }
        public virtual Course Courses { get; set; }

        [Required]
        [Column("letter_grade_id")]
        [ForeignKey("letter_grade_id")]
        public Guid LetterGradeId { get; set; }
        public virtual LetterGrade LetterGrade { get; set; }

        [Required]
        [Column("course_grade")]
        public int CourseGrade { get; set; }
    }
}
