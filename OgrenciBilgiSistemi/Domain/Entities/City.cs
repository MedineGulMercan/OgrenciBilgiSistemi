using OgrenciBilgiSistemi.Domain.Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgrenciBilgiSistemi.Domain.Entities
{
    public class City : BaseEntity
    {
        [Required]
        [Column("city_Name", TypeName = "nvarchar(max)")]
        public string CityName { get; set; }
    }
}
