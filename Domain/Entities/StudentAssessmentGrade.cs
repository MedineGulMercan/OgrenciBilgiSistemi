using EntityLayer.Concrete;
using EntityLayer.Concrete.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class StudentAssessmentGrade : BaseEntity 
    {
        public int AssessmentNote { get; set; }
        public CourseAssessment CourseAssessment { get; set; }
        public Student Student { get; set; }
    }
}
