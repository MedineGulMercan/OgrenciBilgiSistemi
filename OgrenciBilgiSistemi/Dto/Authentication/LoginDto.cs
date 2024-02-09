using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Authentication
{
    public class LoginDto
    {
        public bool Type { get; set; }
        public string TC { get; set; }
        public string Password { get; set; }
        public Guid? RoleId { get; set; }
    }
}
