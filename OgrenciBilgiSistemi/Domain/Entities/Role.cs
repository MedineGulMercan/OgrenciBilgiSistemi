using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OgrenciBilgiSistemi.Domain.Entities.Interface;

namespace OgrenciBilgiSistemi.Domain.Entities
{
    public class Role : BaseEntity
    {
        [Required]
        [Column("role_name", TypeName = "nvarchar(max)")]
        public string RoleName { get; set; }
        
        [Required]
        [Column("role_description", TypeName = "nvarchar(max)")]
        public string RoleDescription { get; set; }
    }
}
