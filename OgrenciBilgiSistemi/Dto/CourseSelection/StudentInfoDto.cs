namespace OgrenciBilgiSistemi.Dto.CourseSelection
{
    public class StudentInfoDto
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public Guid ClassId { get; set; }
        public string ClassName { get; set; }
        public Guid SemesterId { get; set; }
        public string SemesterName { get; set; }
    }
}
