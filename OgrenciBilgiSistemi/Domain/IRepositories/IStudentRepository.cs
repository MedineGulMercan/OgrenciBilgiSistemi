using OgrenciBilgiSistemi.Domain.Entities;

namespace OgrenciBilgiSistemi.Domain.IRepositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student> CreateAsync(Student student);
        decimal GetStudentGano(Guid studentId);
        List<Guid> GetFailedLessons(Guid studentId);
        List<Guid> GetSuccessLessons(Guid studentId);
        LetterGrade GetLetterGrade(Guid courseId, decimal average);
    }
}
