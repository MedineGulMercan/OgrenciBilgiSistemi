using OgrenciBilgiSistemi.Domain.Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciBilgiSistemi.Domain.Entities
{
    public class Student : BaseEntity
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

        [Required]
        [Column("department_id")]
        [ForeignKey("department_id")]
        public Guid DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [Required]
        [Column("city_id")]
        [ForeignKey("city_id")]
        public Guid CityId { get; set; }
        public virtual City City { get; set; }

        [Required]
        [Column("class_id")]
        [ForeignKey("class_id")]
        public Guid ClassId { get; set; }
        public virtual Class Class { get; set; }

        [Required]
        [Column("user_id")]
        [ForeignKey("user_id")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
