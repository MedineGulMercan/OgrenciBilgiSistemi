namespace OgrenciBilgiSistemi.Dto.SemesterCourses
{
    public class SemesterCoursesTableDto
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public Guid ClassId { get; set; }
        public string ClassName { get; set; }
        public Guid SemesterId { get; set; }
        public string SemesterName { get; set; }
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public Guid TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSurname { get; set; }
    }
}
