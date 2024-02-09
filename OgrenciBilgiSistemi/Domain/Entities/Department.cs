using OgrenciBilgiSistemi.Domain.Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciBilgiSistemi.Domain.Entities
{
    public class Department :BaseEntity
    {
        [Required]
        [Column("department_name", TypeName = "nvarchar(max)")]
        public string DepartmentName { get; set; }

        [Required]
        [Column("department_language_id")]
        [ForeignKey("department_language_id")]
        public Guid DepartmentLanguageId { get; set; }
        public virtual DepartmentLanguage DepartmentLanguage { get; set; }

    }
}
