using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemi.Const;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;
using OgrenciBilgiSistemi.Domain.Repositories;
using OgrenciBilgiSistemi.Dto;
using OgrenciBilgiSistemi.Dto.AssessmentType;
using OgrenciBilgiSistemi.Dto.CourseAssessment;
using System.Security.Claims;

namespace OgrenciBilgiSistemi.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = RoleConst.OgretmenRole)]
    public class CourseAssessmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICourseAssessmentRepository _courseAssessmentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IAssessmentTypeRepository _assessmentTypeRepository;
        private readonly ITeacherCourseRepository _teacherCourseRepository;
        private readonly ICourseSelectionRepository _courseSelectionRepository;
        private readonly ICurrentSemesterRepository _currentSemesterRepository;

        public CourseAssessmentController(IMapper mapper,
                                          ICourseAssessmentRepository courseAssessmentRepository,
                                          ICourseRepository courseRepository,
                                          ITeacherRepository teacherRepository,
                                          IAssessmentTypeRepository assessmentTypeRepository,
                                          ITeacherCourseRepository teacherCourseRepository,
                                          ICourseSelectionRepository courseSelectionRepository,
                                          ICurrentSemesterRepository currentSemesterRepository)
        {
            _mapper = mapper;
            _courseAssessmentRepository = courseAssessmentRepository;
            _courseRepository = courseRepository;
            _teacherRepository = teacherRepository;
            _assessmentTypeRepository = assessmentTypeRepository;
            _teacherCourseRepository = teacherCourseRepository;
            _courseSelectionRepository = courseSelectionRepository;
            _currentSemesterRepository = currentSemesterRepository;
        }

        public IActionResult Index(bool success, string message)
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

            ViewBag.Course = _teacherCourseRepository
                .GetAll(x => x.TeacherId == teacherId)
                .Include(x => x.Course)
                .Select(x => new Course
                {
                    Id = x.Course.Id,
                    CourseName = x.Course.CourseCode + " " + x.Course.CourseName,
                });

            ViewBag.AssessmentType = _assessmentTypeRepository.GetAll(x => true);

            if (success)
            {
                ViewBag.Success = message;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CourseAssessmentGetAll()
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
            var data = await _courseAssessmentRepository.CourseAssessmentInnerJoin(teacherId);
            //var data = await _courseAssessmentRepository.GetAll(x => x.TeacherId == teacherId).ToListAsync();

            return Json(data);
        }
        [HttpPost]
        public async Task<IActionResult> CourseAssessmentCreate(CourseAssessmentCreateDto entity)
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

            var studentAny = await _courseSelectionRepository.GetAllTeacherCourse(teacherId).AnyAsync(x => x.Id == entity.CourseId);
            if (studentAny)
            {
                var currentSemester = await _currentSemesterRepository.GetAll(x => x.IsActive).FirstAsync();
                var data = await _courseAssessmentRepository.GetAll(x => x.CurrentSemestersId == currentSemester.Id && x.CourseId == entity.CourseId && x.AssessmentTypeId == entity.AssessmentTypeId).AnyAsync();
                if (!data)
                {
                    var courseAssessment = _mapper.Map<CourseAssessment>(entity);
                    courseAssessment.TeacherId = teacherId;
                    courseAssessment.CurrentSemestersId = currentSemester.Id;
                    courseAssessment = await _courseAssessmentRepository.CreateAsync(courseAssessment);
                    return Json(new Response<CourseAssessment>
                    {
                        Success = true,
                        Message = "Sınav oluşturuldu.",
                    });
                }
                return Json(new Response<CourseAssessment>
                {
                    Success = false,
                    Message = "Bu dönem için zaten sınav oluşturulmuş.",
                });

            }
            return Json(new Response<CourseAssessment>
            {
                Success = false,
                Message = "Derste kayıtlı öğrenci bulunmamakta",
            });
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id, bool success, string message)
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

            var courseassessment = await _courseAssessmentRepository.FirstOrDefaultAsync(x => x.Id == id);

            ViewBag.Course = _teacherCourseRepository
                .GetAll(x => x.TeacherId == teacherId)
                .Include(x => x.Course)
                .Select(x => new Course
                {
                    Id = x.Course.Id,
                    CourseName = x.Course.CourseCode + " " + x.Course.CourseName,
                });

            ViewBag.AssessmentTypes = await _assessmentTypeRepository.GetAll(x => true).ToListAsync() ?? new List<AssessmentType>();

            if (success)
            {
                ViewBag.Success = message;
            }
            return View(courseassessment);
        }
        [HttpPost]
        public async Task<IActionResult> CourseAssessmentUpdate(CourseAssessmentUpdateDto courseAssessmentUpdateDto)
        {
            var courseAssessment = await _courseAssessmentRepository.FirstOrDefaultAsync(x => x.Id == courseAssessmentUpdateDto.Id);
            if (courseAssessment is not null)
            {
                _mapper.Map(courseAssessmentUpdateDto, courseAssessment);
                courseAssessment = await _courseAssessmentRepository.UpdateAsync(courseAssessment);
                return Json(new Response<CourseAssessment>
                {
                    Success = true,
                    Message = "Kayıt Başarılı.",
                    Result = courseAssessment
                });
            }
            return Json(new Response<Class>
            {
                Success = false,
                Message = "Böyle bir sınav türü mevcut değil.",
            });
        }
    }
}
