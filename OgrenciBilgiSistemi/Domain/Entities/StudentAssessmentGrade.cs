using OgrenciBilgiSistemi.Domain.Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciBilgiSistemi.Domain.Entities
{
    public class StudentAssessmentGrade : BaseEntity
    {
        [Required]  
        [Column("student_id")]
        [ForeignKey("student_id")]
        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }

        [Required]
        [Column("assesment_note", TypeName = "int")]
        public int AssessmentNote { get; set; }

        [Required]
        [Column("pass_course", TypeName = "bit")]
        public bool PassCourse { get; set; }


    }
}
