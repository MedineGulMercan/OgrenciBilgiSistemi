namespace OgrenciBilgiSistemi.Dto.CourseLetterScore
{
    public class CourseLetterScoreUpdateDto
    {
        public Guid CourseId { get; set; }
        public Guid LetterGradeId { get; set; }
        public int CourseGrade { get; set; }
    }
}
