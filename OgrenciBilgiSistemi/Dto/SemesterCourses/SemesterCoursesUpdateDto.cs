namespace OgrenciBilgiSistemi.Dto.SemesterCourses
{
    public class SemesterCoursesUpdateDto
    {
        public Guid DepartmentId { get; set; }
        public Guid ClassId { get; set; }
        public Guid SemesterId { get; set; }
        public Guid CourseId { get; set; }
        public Guid TeacherId { get; set; }
    }
}
