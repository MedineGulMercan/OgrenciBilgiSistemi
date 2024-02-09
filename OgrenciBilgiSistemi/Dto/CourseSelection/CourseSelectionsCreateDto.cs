
namespace OgrenciBilgiSistemi.Dto.CourseSelection
{
    public class CourseSelectionsCreateDto
    {
        public Guid StudentId { get; set; }
        public Guid SemesterCoursesId { get; set; }
        public Guid ClassId { get; set; }
        public bool IsRegistrationConfirmed { get; set; }
    }
}
