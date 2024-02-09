using EntityLayer.Concrete.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class TeacherDepartment : BaseEntity 
    {
        public List<Department> Departments { get; set; }
        public Teacher Teacher { get; set; }
    }
}
