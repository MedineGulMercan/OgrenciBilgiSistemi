using EntityLayer.Concrete.Base;


namespace EntityLayer.Concrete
{
    public class Course : BaseEntity 
    {
        public string? CourseName { get; set; }
        public int CourseCredit { get; set; }
        public bool PracticalCourse { get; set; }
    }
}
