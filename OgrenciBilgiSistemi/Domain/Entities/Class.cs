using OgrenciBilgiSistemi.Domain.Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciBilgiSistemi.Domain.Entities
{
    public class Class : BaseEntity
    {
        [Required]
        [Column("class_name", TypeName = "nvarchar(max)")]
        public string ClassName { get; set; }
    }
}
