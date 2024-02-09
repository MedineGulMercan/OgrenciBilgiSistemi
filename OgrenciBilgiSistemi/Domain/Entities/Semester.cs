using OgrenciBilgiSistemi.Domain.Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciBilgiSistemi.Domain.Entities
{
    public class Semester : BaseEntity
    {
        [Required]
        [Column("semester_name", TypeName = "nvarchar(max)")]
        public string SemesterName { get; set; }

    }
}
