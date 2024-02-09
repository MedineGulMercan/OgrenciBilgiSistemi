using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemi.Const;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;
using OgrenciBilgiSistemi.Dto;
using OgrenciBilgiSistemi.Helper;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OgrenciBilgiSistemi.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = RoleConst.OgrenciRole)]
    public class MyNotesController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ISemesterCoursesRepository _semesterCoursesRepository;
        private readonly ICourseAssessmentRepository _courseAssessmentRepository;
        private readonly IExamGradeRepository _examGradeRepository;
        private readonly ICourseSelectionRepository _courseSelectionRepository;
        private readonly IHelper _helper;

        public MyNotesController(IStudentRepository studentRepository,
                                 ISemesterCoursesRepository semesterCoursesRepository,
                                 ICourseAssessmentRepository courseAssessmentRepository,
                                 IExamGradeRepository examGradeRepository,
                                 ICourseSelectionRepository courseSelectionRepository,
                                 IHelper helper)
        {
            _studentRepository = studentRepository;
            _semesterCoursesRepository = semesterCoursesRepository;
            _courseAssessmentRepository = courseAssessmentRepository;
            _examGradeRepository = examGradeRepository;
            _courseSelectionRepository = courseSelectionRepository;
            _helper = helper;
        }

        public IActionResult Index()
        {
            #region student Id
            var studentId = Guid.Empty;
            var userId = HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is not null)
            {
                var student = _studentRepository.FirstOrDefaultAsync(x => x.UserId == Guid.Parse(userId)).Result;
                if (student is not null)
                    studentId = student.Id;
            }
            #endregion

            ViewBag.Semesters = _semesterCoursesRepository
                .GetAll(x => true)
                .Include(x => x.Class)
                .Include(x => x.Semester)
                .AsEnumerable()
                .DistinctBy(x => new { x.ClassId, x.SemesterId });

            ViewBag.Gano = _studentRepository.GetStudentGano(studentId);
            return View();
        }

        public async Task<IActionResult> GetAllMyNotes(Guid classId, Guid semesterId)
        {
            #region student Id
            var studentId = Guid.Empty;
            var userId = HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is not null)
            {
                var student = _studentRepository.FirstOrDefaultAsync(x => x.UserId == Guid.Parse(userId)).Result;
                if (student is not null)
                    studentId = student.Id;
            }
            #endregion

            var data = await (from sc in _semesterCoursesRepository.GetAll(x => x.ClassId == classId && x.SemesterId == semesterId).Include(x => x.Courses)
                              join cs in _courseSelectionRepository.GetAll(x => x.StudentId == studentId) on sc.Id equals cs.SemesterCoursesId

                              join ca in _courseAssessmentRepository.GetAll(x => true).Include(x => x.AssessmentType) on sc.CourseId equals ca.CourseId
                              into caGroup
                              from ca in caGroup.DefaultIfEmpty()

                              join eg in _examGradeRepository.GetAll(x => x.StudentId == studentId)
                              on ca.Id equals eg.CourseAssessmentId
                                     into egGroup
                              from eg in egGroup.DefaultIfEmpty()

                              select new MyNoteDto
                              {
                                  CourseId = sc.CourseId,
                                  CourseName = sc.Courses.CourseName,
                                  CourseNote = ca != null ? (eg != null ? eg.Score * (((decimal)ca.ImpactCourseGrade) / 100) : 0) : 0,
                                  CourseNoteString = ca != null ? (eg != null ? ca.AssessmentType.AssessmentTypeName + ": " + eg.Score.ToString() : ca.AssessmentType.AssessmentTypeName + ": Not Girilmedi") : "Sınav oluşturulmadı",
                              })
                    .GroupBy(x => new { x.CourseId, x.CourseName })
                    .Select(group => new MyNotesDto
                    {
                        CourseId = group.Key.CourseId,
                        CourseName = group.Key.CourseName,
                        CourseNotesString = group.Select(item => item.CourseNoteString).ToList(),
                        CourseNotes = group.Select(item => item.CourseNote).ToList(),
                    })
                    .ToListAsync();

            foreach (var item in data)
            {
                item.CourseAvarage = Convert.ToDouble(item.CourseNotes.Sum());
                item.LetterNote = _studentRepository.GetLetterGrade(item.CourseId, (decimal)item.CourseAvarage).Letter;
                if (item.LetterNote == "FF")
                {
                    item.GectiMi = "Kaldı";
                }
                else
                {
                    item.GectiMi = "Geçti"; 
                }
            }

            return Json(data);
        }
    }
}
