using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemi.Const;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;
using OgrenciBilgiSistemi.Dto;
using OgrenciBilgiSistemi.Dto.CourseSelection;
using OgrenciBilgiSistemi.Helper;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace OgrenciBilgiSistemi.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = RoleConst.OgrenciRole)]
    public class CourseSelectionController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ISemesterCoursesRepository _semesterCoursesRepository;
        private readonly ICurrentSemesterRepository _currentSemesterRepository;
        private readonly ITeacherCourseRepository _teacherCourseRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISemesterRepository _semesterRepository;
        private readonly IMapper _mapper;
        private readonly ICourseSelectionRepository _courseSelectionRepository;
        private readonly IExamGradeRepository _examGradeRepository;
        private readonly ICourseAssessmentRepository _courseAssessmentRepository;
        private readonly IHelper _helper;

        public CourseSelectionController(ICourseRepository courseRepository,
                                         IStudentRepository studentRepository,
                                         IDepartmentRepository departmentRepository,
                                         ISemesterCoursesRepository semesterCoursesRepository,
                                         ICurrentSemesterRepository currentSemesterRepository,
                                         ITeacherCourseRepository teacherCourseRepository,
                                         ITeacherRepository teacherRepository,
                                         IClassRepository classRepository,
                                         ISemesterRepository semesterRepository,
                                         IMapper mapper,
                                         ICourseSelectionRepository courseSelectionRepository,
                                         IExamGradeRepository examGradeRepository,
                                         ICourseAssessmentRepository courseAssessmentRepository,
                                         IHelper helper)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _departmentRepository = departmentRepository;
            _semesterCoursesRepository = semesterCoursesRepository;
            _currentSemesterRepository = currentSemesterRepository;
            _teacherCourseRepository = teacherCourseRepository;
            _teacherRepository = teacherRepository;
            _classRepository = classRepository;
            _semesterRepository = semesterRepository;
            _mapper = mapper;
            _courseSelectionRepository = courseSelectionRepository;
            _examGradeRepository = examGradeRepository;
            _courseAssessmentRepository = courseAssessmentRepository;
            _helper = helper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(bool success, dynamic message)
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

            //öğrenci bilgileri
            var studentInfo = await _studentRepository.FirstOrDefaultAsync(x => x.Id == studentId); 

            // öğrencinin ganosunu almak için değişken oluşturuyoruz 
            decimal gradeNoteAverage = 0;
            // öğrenci daha önce gano hesaplanması için sistemde kayıtlı aldığı bir not var mı diye bakıyoruz var ise 
            var examGradeNote = _examGradeRepository.GetAll(x => x.StudentId == studentId).Select(x => x.Score);
            if (examGradeNote.Any())
            {
                // gano hesabını getiriyoruz
                gradeNoteAverage = _studentRepository.GetStudentGano(studentId);
            }

            // kaldığı dersleri getiriyor
            var failedLessons = _studentRepository.GetFailedLessons(studentId);
            var successLessons = _studentRepository.GetSuccessLessons(studentId);

            // bulunduğumuz yılın içindeki bulunduğumuz semesterı çekiyoruz
            var currentSemester = await _currentSemesterRepository
                .GetAll(x => x.IsActive)
                .Include(x=>x.Semester)
                .FirstAsync();

            // ders statusleri belirliyoruz 1 ise kaldı 2 ise dönem dersi 3 ise üstten dersler listeleniyor
            // öğrenci ganosu 2 ve üzerinde ise bir sonraki sınıftaki dersleri getiriyor
            var datax = await (from sc in _semesterCoursesRepository.GetAll(x => x.DepartmentId == studentInfo.DepartmentId
                                                                              && x.SemesterId == currentSemester.SemesterId
                                                                              // bu where koşulu eğer öğrencinin sınıfına ait bir ders ise getir ortalamsını 2 ver üzerindeyse diğer 
                                                                              // üstten alabileceği dersleri getir demek 
                                                                              && (x.ClassId == studentInfo.ClassId || (gradeNoteAverage >= 0.5m))
                                                                              && !successLessons.Contains(x.CourseId)
                                                                              )
                               join cl in _classRepository.GetAll(x => true) on sc.ClassId equals cl.Id
                               join t in _teacherRepository.GetAll(x => true) on sc.TeacherId equals t.Id
                               join c in _courseRepository.GetAll(x => true) on sc.CourseId equals c.Id
                               select new CourseSelectionDto
                               {
                                   Id = sc.Id,
                                   CourseId = sc.CourseId,
                                   CourseName = c.CourseName,
                                   TeacherId = sc.TeacherId,
                                   TeacherNameSurname = c.CourseName + " " + t.Name + ' ' + t.Surname + " Kredi: " + c.CourseCredit,
                                   CourseCredit = c.CourseCredit,
                                   CourseStatus = failedLessons.Any(x=>x == c.Id) ? 1 : sc.ClassId == studentInfo.ClassId ? 2 : 3
                               }).ToListAsync();

            ViewBag.CourseSelectionData = datax;

            var classInfo = _classRepository.FirstOrDefaultAsync(x => x.Id == studentInfo.ClassId).Result;
            var semesterInfo = _currentSemesterRepository.FirstOrDefaultAsync(x => x.IsActive).Result;
            var semesterr = _semesterRepository.FirstOrDefaultAsync(x => x.Id == semesterInfo.SemesterId).Result;
            StudentInfoDto studentInfoDto = new()
            {
                ClassId = studentInfo.ClassId,
                ClassName = classInfo.ClassName,
                StudentName = studentInfo.Name + studentInfo.Surname,
                SemesterName = currentSemester.Semester.SemesterName,
            };

            // daha önce seçilen öğrencinin id 'sine göre ve bulunduğu sınıf ve semestırına göre seçtiği dersleri getiriyorum
            var courseSelectedData = _courseSelectionRepository.GetAllInnerJoin(studentId, studentInfo.ClassId, currentSemester.SemesterId).ToListAsync().Result;
            ViewBag.CourseSelection = courseSelectedData;

            // öğrencinin seçtiği derslerin toplam kredisini getiriyor
            ViewBag.SumCredit = courseSelectedData.Any() ? courseSelectedData.Select(x => x.CourseCredit).Sum() : 0;
            if (success)
            {
                ViewBag.Success = message;
            }

            // öğrenci bulunduğu sınıf ve semesterda seçtipi ders var mı kontrolu yapıyor ona göre true false döndürüyor
            ViewBag.OnayButton = courseSelectedData.Any();

            return View(studentInfoDto);
        }

        [HttpPost]
        public async Task<IActionResult> CourseSelectionCreate(List<CourseSelectionsCreateDto> courseSelectionsCreateDto)
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

            var data = await _studentRepository.FirstOrDefaultAsync(x => x.Id == studentId);
            foreach (var item in courseSelectionsCreateDto)
            {
                item.StudentId = studentId;
                item.IsRegistrationConfirmed = false;
                item.ClassId = data.ClassId;

            }

            if (courseSelectionsCreateDto.Any())
            {
                var courseSelection = _mapper.Map<List<CourseSelection>>(courseSelectionsCreateDto);
                await _courseSelectionRepository.ListCreateAsync(courseSelection);
                ViewBag.ButtonClicked = true;
                return Json(new Response<List<CourseSelection>>
                {
                    Success = true,
                    Message = "Kayıt Başarılı",
                    Result = courseSelection
                });
            }
            return Json(new Response<CourseSelection>
            {
                Success = false,
                Message = "Kayıt Başarısız",
            });
        }
    }
}
