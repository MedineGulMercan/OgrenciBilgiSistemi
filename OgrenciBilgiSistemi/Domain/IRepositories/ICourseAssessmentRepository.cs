using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Dto.CourseAssessment;

namespace OgrenciBilgiSistemi.Domain.IRepositories
{
    public interface ICourseAssessmentRepository : IRepository<CourseAssessment>
    {
        Task<List<CourseAssessmentDto>> CourseAssessmentInnerJoin( Guid? id);
    }
}
