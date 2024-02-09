using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using OgrenciBilgiSistemi.Domain.DataBaseContext;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;
using OgrenciBilgiSistemi.Dto.CourseAssessment;
using OgrenciBilgiSistemi.Dto.TeacherCourse;

namespace OgrenciBilgiSistemi.Domain.Repositories
{
    public class CourseAssessmentRepository : Repository<CourseAssessment>, ICourseAssessmentRepository

    {
        private readonly Context _context;
        public CourseAssessmentRepository(Context context) : base(context)
        {
            _context = context;

        }

        public async Task<List<CourseAssessmentDto>> CourseAssessmentInnerJoin(Guid? id)
        {
            if(id is not null)
            {
                var alldata = await (from ca in _context.CourseAssessments
                                     where ca.TeacherId == id
                                     join c in _context.Courses on ca.CourseId equals c.Id
                                     join at in _context.AssessmentTypes on ca.AssessmentTypeId equals at.Id
                                     select new CourseAssessmentDto
                                     {
                                         Id = ca.Id,
                                         CourseId = c.Id,
                                         AssessmentTypeId = at.Id,
                                         AssessmentName = ca.AssessmentName,
                                         PassingScore = ca.PassingScore,
                                         CourseName = c.CourseName,
                                         AssessmentTypeName = at.AssessmentTypeName,

                                     }).ToListAsync();
                return alldata;
            }
            return new List<CourseAssessmentDto>();
        }
    }
}
