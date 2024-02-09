using OgrenciBilgiSistemi.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OgrenciBilgiSistemi.Domain.Entities.Interface;

namespace OgrenciBilgiSistemi.Domain.Entities
{
    public class Teacher : BaseEntity
    {
        [Required]
        [Column("name", TypeName = "nvarchar(max)")]
        public string Name { get; set; }

        [Required]
        [Column("surname", TypeName = "nvarchar(max)")]
        public string Surname { get; set; }

        [Required]
        [Column("tc", TypeName = "nvarchar(11)")]
        public string TC { get; set; }

        [Required]
        [Column("email_address", TypeName = "nvarchar(max)")]
        public string EmailAddress { get; set; }

        [Required]
        [Column("birthday")]
        public DateTime Birthday { get; set; }

        [Required]
        [Column("phone_number", TypeName = "nvarchar(max)")]
        public string PhoneNumber { get; set; }

        [Column("user_id")]
        [ForeignKey("user_id")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [Column("city_id")]
        [ForeignKey("city_id")]
        public Guid CityId { get; set; }
        public virtual City City { get; set; }
    }
}
