using EntityLayer.Concrete.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Teacher : BaseEntity
    {
        public string? TeacherName { get; set; }
        public string? TeacherSurname { get; set; }
        public DateTime TeacherBirthday { get; set; }
        public string? TeacherPhoneNumber { get; set; }
        public User User { get; set; }
        public City City { get; set; }
    }
}
