using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemi.Domain.DataBaseContext;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;

namespace OgrenciBilgiSistemi.Domain.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private readonly Context _context;
        public StudentRepository(Context context) : base(context)
        {
            _context = context;
        }

        public decimal GetStudentGano(Guid studentId)
        {
            // student'ın notları girilmiş derslerini ve o derse oluşturulmuş sınavlarını çekiyoruz sonra ders id'ye göre grupluyoruz
            var data = _context.ExamGrades
                .Where(x => x.StudentId == studentId)
                .Include(x => x.CourseAssessment)
                .Include(x => x.CourseAssessment.Courses)
                .GroupBy(x => new { x.CourseAssessment.CourseId, x.CourseAssessment.Courses.CourseCredit })
                // sonra sınavın yüzde kaç etkili olduğu bilgisinin alıp ders göre grupladığımız derslerin yüzdelik etkisini alıp topluyoruz
                .Select(group => new
                {
                    CourseId = group.Key.CourseId,
                    // score impactCourseGrade int olduğundan 100 e bölerken sıkıntı oluyordu decimal convert ile çözüldü 
                    // CourseAssessment.ImpactCourseGrade = sınavın ortamalaya etkisi 
                    CourseAverage = group.Sum(x => x.Score * ((decimal)x.CourseAssessment.ImpactCourseGrade) / 100),
                    CourseCredit = group.Key.CourseCredit,
                });

            // gelen harf notu karşılığı ve kredi toplamını alıp ganosunu hesaplıyoruz 
            int creditSum = 0;
            decimal gradeSum = 0;
            foreach (var item in data)
            {
                creditSum += item.CourseCredit;
                // method dersin id si ve dersin ortalamaya göre harf notu hesaplıyıp döndürüyor
                gradeSum += GetLetterGrade(item.CourseId, item.CourseAverage).Grade;
            }

            // kredi toplamına 4 lük sayı ile hesaplanmış ders ortalamasının bölümü
            var result = gradeSum / (decimal)creditSum;

            return result;
        }
        //KALDIĞI DERSLERİN LİSTESİ
        public List<Guid> GetFailedLessons(Guid studentId)
        {
            // student'ın derslerini çekiyorum ve ortalamalarını alıyorum
            var data = _context.ExamGrades
                .Where(x => x.StudentId == studentId)
                .Include(x => x.CourseAssessment)
                .Include(x => x.CourseAssessment.Courses)
                .Include(x => x.CourseAssessment.Courses.CourseLetterScores)
                .GroupBy(x => x.CourseAssessment.CourseId)
                .Select(group => new
                {
                    CourseId = group.Key,
                    CourseAverage = group.Sum(x => x.Score * (((decimal)x.CourseAssessment.ImpactCourseGrade) / 100)),
                });

            // sonra foreach ile ortalamaları dönerek CourseLetterScores içerisinde öğretmenin tanımladığı
            // harf not aralığına göre ders puanının harf not karşılığını çekiyorum
            var list = new List<Guid>();
            foreach (var item in data)
            {
                // dersin ortalamasının harf notu karşılığı ff ise kaldığı derslerin id'sini list'e atıyorum 
                var letter = _context.CourseLetterScores
                    .Include(x => x.LetterGrade)
                    .OrderBy(x => x.LetterGrade.OrderBy)
                    .Where(x => x.CourseId == item.CourseId && item.CourseAverage >= x.CourseGrade)
                    .FirstOrDefaultAsync().Result;
                if (letter != null && (letter.LetterGrade.Letter == "FF"))
                {
                    list.Add(letter.CourseId);
                }
            }

            return list;
        }
        //GEÇTİĞİ DERSLERİN LİSTESİ 
        public List<Guid> GetSuccessLessons(Guid studentId)
        {
            // student'ın derslerini çekiyorum ve ortalamalarını alıyorum
            var data = _context.ExamGrades
                .Where(x => x.StudentId == studentId)
                .Include(x => x.CourseAssessment)
                .Include(x => x.CourseAssessment.Courses)
                .Include(x => x.CourseAssessment.Courses.CourseLetterScores)
                .GroupBy(x => x.CourseAssessment.CourseId)
                .Select(group => new
                {
                    CourseId = group.Key,
                    CourseAverage = group.Sum(x => x.Score * (((decimal)x.CourseAssessment.ImpactCourseGrade) / 100)),
                });

            // sonra foreach ile ortalamaları dönerek CourseLetterScores içerisinde öğretmenin tanımladığı
            // harf not aralığına göre ders puanının harf not karşılığını çekiyorum
            var list = new List<Guid>();
            foreach (var item in data)
            {
                // dersin ortalamasının harf notu karşılığı ff ise kaldığı derslerin id'sini list'e atıyorum 
                var letter = _context.CourseLetterScores
                    .Include(x => x.LetterGrade)
                    .OrderBy(x => x.LetterGrade.OrderBy)
                    .Where(x => x.CourseId == item.CourseId && item.CourseAverage >= x.CourseGrade)
                    .FirstOrDefaultAsync().Result;
                if (letter != null && (letter.LetterGrade.Letter != "FF"))
                {
                    list.Add(letter.CourseId);
                }
            }

            return list;
        }

        public LetterGrade GetLetterGrade(Guid courseId, decimal average)
        {
            // dersin id'sine göre ve ortalamasına göre harf notunu çekiyorum
            var letter = _context.CourseLetterScores
                      .FirstOrDefaultAsync(x => x.CourseId == courseId && average >= x.CourseGrade)
                      .Result
                      .LetterGradeId;

            // sonra harf notlarının 4 lük puan karşılığını çekiyorum ve return ediyorum 
            return _context.LetterGrades.FirstOrDefaultAsync(x => x.Id == letter).Result;
        }
    }
}
