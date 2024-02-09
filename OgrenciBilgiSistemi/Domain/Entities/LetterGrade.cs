using OgrenciBilgiSistemi.Domain.Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciBilgiSistemi.Domain.Entities
{
    public class LetterGrade : BaseEntity
    {
        [Required]
        [Column("letter", TypeName = "nvarchar(max)")]
        public string Letter { get; set; }

        [Required]
        [Column("grade", TypeName = "decimal(18, 2)")]
        public decimal Grade { get; set; }

        [Required]
        [Column("order_by")]
        public int OrderBy { get; set; }
    }
}
