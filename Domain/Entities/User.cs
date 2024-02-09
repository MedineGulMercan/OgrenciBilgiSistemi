using EntityLayer.Concrete.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class User :BaseEntity
    {
        [Column("tc",TypeName= "nvarchar(11)") ]
        public string? TC { get; set; }
        public string Password { get; set; }
        public string? EmailAddress { get; set; }
        public Role Role { get; set; }
    }
}
