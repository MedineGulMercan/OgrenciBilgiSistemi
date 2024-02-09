using OgrenciBilgiSistemi.Domain.Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciBilgiSistemi.Domain.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [Column("password", TypeName = "nvarchar(max)")]
        public string Password { get; set; }

        [Required]
        [Column("user_name", TypeName = "nvarchar(11)")]
        public string UserName { get; set; }

        [Required]
        [Column("role_id")]
        [ForeignKey("role_id")]
        public Guid RoleId { get; set; }

        public virtual Role Role { get; set; }

    }
}
