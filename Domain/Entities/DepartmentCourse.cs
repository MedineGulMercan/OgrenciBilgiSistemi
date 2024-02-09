using EntityLayer.Concrete.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class DepartmentCourse : BaseEntity
    {
        public List<Course> Courses { get; set; }

        public Department Departments { get; set; }
    }
}
