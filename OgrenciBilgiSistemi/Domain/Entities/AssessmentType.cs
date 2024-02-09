using OgrenciBilgiSistemi.Domain.Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciBilgiSistemi.Domain.Entities
{
    public class AssessmentType : BaseEntity
    {
        [Required]
        [Column("assessment_type_name", TypeName = "nvarchar(max)")] 
        public string? AssessmentTypeName { get; set; }
    }
}
