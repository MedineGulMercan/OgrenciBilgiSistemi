
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete.Base
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } 
    }
}
