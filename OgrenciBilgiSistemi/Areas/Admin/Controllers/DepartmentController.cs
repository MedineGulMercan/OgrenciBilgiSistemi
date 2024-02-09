using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemi.Const;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;
using OgrenciBilgiSistemi.Domain.Repositories;
using OgrenciBilgiSistemi.Dto;
using OgrenciBilgiSistemi.Dto.Class;
using OgrenciBilgiSistemi.Dto.Department;
using OgrenciBilgiSistemi.Dto.SemesterCourses;

namespace OgrenciBilgiSistemi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConst.AdminRole)]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDepartmentLanguageRepository _departmentLanguageRepository;
        private readonly IMapper _mapper;
        private readonly ISemesterRepository _semesterRepository;
        private readonly IClassRepository _classRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ISemesterCoursesRepository _semesterCoursesRepository;
        private readonly ITeacherCourseRepository _teacherCourseRepository;
        private readonly ITeacherRepository _teacherRepository;
        public DepartmentController(IDepartmentRepository departmentRepository,
                                    IMapper mapper,
                                    IDepartmentLanguageRepository departmentLanguageRepository,
                                    ISemesterRepository semesterRepository,
                                    IClassRepository classRepository,
                                    ICourseRepository courseRepository,
                                    ISemesterCoursesRepository semesterCoursesRepository,
                                    ITeacherCourseRepository teacherCourseRepository,
                                    ITeacherRepository teacherRepository)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _departmentLanguageRepository = departmentLanguageRepository;
            _semesterRepository = semesterRepository;
            _classRepository = classRepository;
            _courseRepository = courseRepository;
            _semesterCoursesRepository = semesterCoursesRepository;
            _teacherCourseRepository = teacherCourseRepository;
            _teacherRepository = teacherRepository;
        }

        [HttpGet]
        public IActionResult Index(bool success, string message)
        {
            ViewBag.Language = _departmentLanguageRepository.GetAll(x => true);
            if (success)
            {
                ViewBag.Success = message;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DepartmentCreate(DepartmentCreateDto departmentCreateDto)
        {
            var department = _mapper.Map<Department>(departmentCreateDto);
            department = await _departmentRepository.CreateAsync(department);
            return Json(new Response<Department>
            {
                Success = true,
                Message = "Kayıt Başarılı",
                Result = department
            });
        }

        [HttpPost]
        public async Task<IActionResult> DepartmentGetAll()
        {
            var data = await _departmentRepository.GetAll(x => true).ToListAsync();
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id, bool success, string message)
        {
            var course = await _departmentRepository.FirstOrDefaultAsync(x => x.Id == id);

            ViewBag.Language = await _departmentLanguageRepository.GetAll(x => true).ToListAsync() ?? new List<DepartmentLanguage>();
            ViewBag.Classes = await  _classRepository.GetAll(x => true).ToListAsync() ?? new List<Class>();
            ViewBag.Semesters = await _semesterRepository.GetAll(x=> true).ToListAsync() ?? new List<Semester>();
            ViewBag.Courses = await _courseRepository.GetAll(x=> true).ToListAsync() ?? new List<Course>();

            ViewBag.SemesterCoursess = await _semesterCoursesRepository
                                                    .GetAllInnerJoin()
                                                    .Where(x => x.DepartmentId == id)
                                                    .ToListAsync() ?? new List<SemesterCoursesTableDto>();
            if (success)
            {
                ViewBag.Success = message;
            }
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> DepartmentUpdate(DepartmentUpdateDto departmentUpdateDto)
        {
            var data = await _departmentRepository.FirstOrDefaultAsync(x => x.Id == departmentUpdateDto.Id);
            if (data is not null)
            {
                _mapper.Map(departmentUpdateDto, data);
                data = await _departmentRepository.UpdateAsync(data);
                return Json(new Response<Department>
                {
                    Success = true,
                    Message = "Kayıt Başarılı.",
                    Result = data
                });
            }
            return Json(new Response<Department>
            {
                Success = false,
                Message = "Böyle bir bölüm mevcut değil.",
            });
        }
        [HttpPost]
        public async Task<IActionResult> SemesterCoursesTableUpdate(List<SemesterCoursesUpdateDto> semesterCoursesUpdateDto)
        {
            if (semesterCoursesUpdateDto.Any())
            {
                var entity = _mapper.Map<List<SemesterCourses>>(semesterCoursesUpdateDto);
                try
                {
                    await _semesterCoursesRepository.DeleteAsync(x => x.DepartmentId == semesterCoursesUpdateDto.First().DepartmentId);
                    await _semesterCoursesRepository.ListCreateAsync(entity);

                }
                catch (Exception)
                {

                    throw;
                }
                return Json(new Response<List<SemesterCourses>>
                {
                    Success = true,
                    Message = "Kayıt Başarılı.",
                    Result = entity
                });
            }
            return Json(new Response<SemesterCourses>
            {
                Success = false,
                Message = "Güncelleme başarısız.",
            });
        }

        [HttpPost]
        public async Task<IActionResult> GetTeachersByCourse(Guid id)
        {
            var coursesByTeachers = _teacherCourseRepository.GetAll(x => x.CourseId == id); 
            if (coursesByTeachers.Any())
            {
                var teachers = coursesByTeachers.Select(tc => tc.Teacher).ToList();
                return Json(new
                {
                    Success = true,
                    Teachers = teachers
                });
            }
                return Json(new Response<Department>
                {
                    Success = false,
                    Message = "Böyle bir bölüm mevcut değil.",
                });
            
        }
    }
}
