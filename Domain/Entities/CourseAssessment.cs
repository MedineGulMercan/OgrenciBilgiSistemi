using EntityLayer.Concrete.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class CourseAssessment : BaseEntity
    {
        public string? AssessmentName { get; set; }
        public int PassingScore { get; set; }
        public Course? Courses { get; set; }
    }
}
