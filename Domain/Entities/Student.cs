using EntityLayer.Concrete.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Student : BaseEntity
    {
        public string? StudentName { get; set; }
        public string? StudentSurname { get; set; }
        public DateTime StudentBirthday { get; set; }
        public int StudentPhoneNumber { get; set; }
        public int StudentNumber { get; set; }
        public City  City { get; set; }
        public Class  Class { get; set; }
        public User User { get; set; }
    }
}
