namespace OgrenciBilgiSistemi.Dto.Course
{
    public class CourseCreateDto
    {
        public string CourseName { get; set; }
        public int CourseCredit { get; set; }
        public int CourseAkts { get; set; }
        public string CourseCode { get; set; }
        public bool PracticalCourse { get; set; }
        public bool PreparationCourse { get; set; }
    }
}
