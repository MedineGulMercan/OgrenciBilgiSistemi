using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemi.Domain.IRepositories;

namespace OgrenciBilgiSistemi.Helper
{
    public class Helper : IHelper
    {
        private readonly ICurrentSemesterRepository _currentSemesterRepository;
        public Helper(ICurrentSemesterRepository currentSemesterRepository)
        {
            _currentSemesterRepository = currentSemesterRepository;
        }
        public (string SemesterName, string Year) GetSemesterAndYear()
        {
            var data = _currentSemesterRepository.GetAll(x => x.IsActive).Include(x => x.Semester).FirstOrDefaultAsync().Result;
            return (data.Semester.SemesterName, data.AssessmentYear);
        }
    }
}
