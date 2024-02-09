namespace OgrenciBilgiSistemi.Dto.CourseSelection
{
    public class CourseSelectionDto
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }
        public Guid DepartmentId { get; set; }
        public string CourseName { get; set; }
        public string DepartmentName { get; set; }
        public string StudentName { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSurname { get; set; }
        public string TeacherNameSurname { get; set; }
        public Guid CurrentSemesterId { get; set; }
        public string CurrentSemesterName { get; set; }
        public Guid ClassId { get; set; }
        public string ClassName { get; set; }
        public int CourseCredit { get; set; }
        public int? CourseScore { get; set; }
        public int? PassingScore { get; set; }
        public int CourseStatus { get; set; }
    }
}
