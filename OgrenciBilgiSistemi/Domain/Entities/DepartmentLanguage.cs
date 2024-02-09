using OgrenciBilgiSistemi.Domain.Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciBilgiSistemi.Domain.Entities
{
    public class DepartmentLanguage : BaseEntity
    {
        [Required]
        [Column("language_name", TypeName = "nvarchar(max)")]
        public string LanguageName { get; set; }
    }
}
