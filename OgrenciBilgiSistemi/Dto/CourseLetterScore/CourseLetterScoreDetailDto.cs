namespace OgrenciBilgiSistemi.Dto.CourseLetterScore
{
    public class CourseLetterScoreDetailDto
    {
        public Guid CourseId { get; set; }
        public string Letter { get; set; }
        public Guid LetterGradeId { get; set; }
        public int CourseGrade { get; set; }
        public int LetterOrderBy { get; set; }
    }
}
