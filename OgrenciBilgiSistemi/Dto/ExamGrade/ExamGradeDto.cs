namespace OgrenciBilgiSistemi.Dto.ExamGrade
{
    public class ExamGradeDto
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public string AssessmentTypeName { get; set; }
        public Guid CourseAssessmentId { get; set; }
        public int Score { get; set; }
    }
}
