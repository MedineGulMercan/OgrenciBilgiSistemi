namespace OgrenciBilgiSistemi.Dto.CourseAssessment
{
    public class CourseAssessmentDto
    {
        public Guid Id { get; set; }
        public string AssessmentName { get; set; }
        public int PassingScore { get; set; }
        public int ImpactCourseGrade { get; set; }
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public string AssessmentTypeName { get; set; }
        public Guid AssessmentTypeId { get; set; }
    }
}
