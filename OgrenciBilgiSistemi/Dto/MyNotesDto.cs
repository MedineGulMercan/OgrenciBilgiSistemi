namespace OgrenciBilgiSistemi.Dto
{
    public class MyNoteDto
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseNoteString { get; set; }
        public decimal CourseNote { get; set; }
    }
    public class MyNotesDto
    {  
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public List<string> CourseNotesString { get; set; }
        public List<decimal> CourseNotes { get; set; }
        public double CourseAvarage { get; set; }
        public string LetterNote { get; set; }
        public string GectiMi { get; set; }
    }
}
