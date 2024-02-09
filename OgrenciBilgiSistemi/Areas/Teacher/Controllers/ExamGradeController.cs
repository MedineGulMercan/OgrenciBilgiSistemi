using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemi.Const;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;
using OgrenciBilgiSistemi.Domain.Repositories;
using OgrenciBilgiSistemi.Dto;
using OgrenciBilgiSistemi.Dto.ExamGrade;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace OgrenciBilgiSistemi.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = RoleConst.OgretmenRole)]
    public class ExamGradeController : Controller
    {
        private readonly ICourseSelectionRepository _courseSelectionRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseAssessmentRepository _courseAssessmentRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IExamGradeRepository _examGradeRepository;
        private readonly ITeacherCourseRepository _courseCourseRepository;
        private readonly ICurrentSemesterRepository _currentSemesterRepository;

        public ExamGradeController(ICourseSelectionRepository courseSelectionRepository,
                                   IStudentRepository studentRepository,
                                   ICourseAssessmentRepository courseAssessmentRepository,
                                   ITeacherRepository teacherRepository,
                                   IExamGradeRepository examGradeRepository,
                                   ITeacherCourseRepository courseCourseRepository,
                                   ICurrentSemesterRepository currentSemesterRepository)
        {
            _courseSelectionRepository = courseSelectionRepository;
            _studentRepository = studentRepository;
            _courseAssessmentRepository = courseAssessmentRepository;
            _teacherRepository = teacherRepository;
            _examGradeRepository = examGradeRepository;
            _courseCourseRepository = courseCourseRepository;
            _currentSemesterRepository = currentSemesterRepository;
        }

        public IActionResult Index()
        {
            #region Teacher Id
            var teacherId = Guid.Empty;
            var userId = HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is not null)
            {
                var teacher = _teacherRepository.FirstOrDefaultAsync(x => x.UserId == Guid.Parse(userId)).Result;
                if (teacher is not null)
                    teacherId = teacher.Id;
            }
            #endregion

            ViewBag.Course = _courseCourseRepository
                .GetAll(x => x.TeacherId == teacherId)
                .Include(x => x.Course)
                .Select(x => new Course
                {
                    Id = x.Course.Id,
                    CourseName = x.Course.CourseCode + " " + x.Course.CourseName,
                });

            ViewBag.Year = _currentSemesterRepository.GetAll(x => true).Include(x => x.Semester).OrderByDescending(x=>x.AssessmentYear);

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddExamGrade(ExamGrade examGrade)
        {
            var data = await _examGradeRepository.FirstOrDefaultAsync(x => x.StudentId == examGrade.StudentId && x.CourseAssessmentId == examGrade.CourseAssessmentId);
            if (data is not null)
            {
                data.Score = examGrade.Score;
                data = await _examGradeRepository.UpdateAsync(data);
            }
            else
            {
                data = await _examGradeRepository.CreateAsync(examGrade);
            }
            return Json(new Response<ExamGrade>
            {
                Success = true,
                Message = "Sınav notu kayıt edildi.",
            });
        }
        [HttpPost]
        public async Task<IActionResult> GetAllStudentCourse(Guid courseId, Guid currentSemesterId)
        {
            #region Teacher Id
            var teacherId = Guid.Empty;
            var userId = HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is not null)
            {
                var teacher = _teacherRepository.FirstOrDefaultAsync(x => x.UserId == Guid.Parse(userId)).Result;
                if (teacher is not null)
                    teacherId = teacher.Id;
            }
            #endregion

            var data = await (from cs in _courseSelectionRepository.GetAll(x => true).Include(x => x.SemesterCourses)
                              join st in _studentRepository.GetAll(x => true) on cs.StudentId equals st.Id
                              join ca in _courseAssessmentRepository.GetAll(x => x.CurrentSemestersId == currentSemesterId && x.TeacherId == teacherId)
                                                                    .Include(x=>x.AssessmentType) 
                              on cs.SemesterCourses.CourseId equals ca.CourseId

                              join eg in _examGradeRepository.GetAll(x => true) 
                              on new { CaId = ca.Id, StId = st.Id } equals new { CaId = eg.CourseAssessmentId, StId = eg.StudentId}
                              into egGroup
                              from eg in egGroup.DefaultIfEmpty()
                              where ca.CourseId == courseId
                              select new ExamGradeDto
                              {
                                  Score = eg != null ? eg.Score : 0,
                                  StudentId = st.Id,
                                  AssessmentTypeName = ca.AssessmentType.AssessmentTypeName,
                                  CourseAssessmentId = ca.Id,
                                  StudentName = st.Name + " " + st.Surname,
                              }).ToListAsync();
            return Json(data);
        }
    }
}
