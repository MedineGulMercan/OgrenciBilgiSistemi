using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OgrenciBilgiSistemi.Domain.Entities.Interface;

namespace OgrenciBilgiSistemi.Domain.Entities
{
    public class Course : BaseEntity 
    {
        [Required]
        [Column("course_name", TypeName = "nvarchar(max)")]
        public string CourseName { get; set; }

        [Required]
        [Column("course_credit", TypeName = "nvarchar(max)")] 
        public int CourseCredit { get; set; }

        [Required]
        [Column("course_akts")]
        public int CourseAkts { get; set; }  
        
        [Required]
        [Column("course_code", TypeName = "nvarchar(max)")]
        public string CourseCode { get; set; }

        //Uygulamalı mı?
        [Required]
        [Column("practical_course")]
        public bool PracticalCourse { get; set; }

        //hazırlık dersi mi?
        [Required]
        [Column("preparation_course ")]
        public bool PreparationCourse { get; set; }

        public virtual ICollection<CourseLetterScore> CourseLetterScores { get; set; }

    }
}
