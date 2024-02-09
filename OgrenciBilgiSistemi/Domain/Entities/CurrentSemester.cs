using Microsoft.Build.Framework;
using OgrenciBilgiSistemi.Domain.Entities.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciBilgiSistemi.Domain.Entities
{
    public class CurrentSemester : BaseEntity
    {// burda tablo işte biliyorum e yok dedin 
        [Required]
        [Column("semesterId")]
        [ForeignKey("semesterId")]
        public Guid SemesterId { get; set; }
        public virtual Semester Semester { get; set; }

        [Required]
        [Column("assesment_year", TypeName = "nvarchar(max)")]
        public string? AssessmentYear { get; set; }
        
        [Required]
        [Column("is_active")]
        public bool IsActive { get; set; }
    }
}
