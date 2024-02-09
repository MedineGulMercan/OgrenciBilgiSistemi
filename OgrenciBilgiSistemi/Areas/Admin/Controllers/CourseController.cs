using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemi.Const;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;
using OgrenciBilgiSistemi.Dto;
using OgrenciBilgiSistemi.Dto.Course;
using OgrenciBilgiSistemi.Dto.CourseLetterScore;
using OgrenciBilgiSistemi.Dto.TeacherCourse;
using OgrenciBilgiSistemi.Dto.User;

namespace OgrenciBilgiSistemi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConst.AdminRole)]
    public class CourseController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICourseRepository _courseRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly ITeacherCourseRepository _teacherCourseRepository;
        private readonly ISemesterCoursesRepository _semesterCoursesRepository;
        private readonly ICourseLetterScoreRepository _courseLetterScoreRepository;
        private readonly ILetterGradeRepository _letterGradeRepository;
        public CourseController(IMapper mapper,
                                ICourseRepository courseRepository,
                                ITeacherRepository teacherRepository,
                                ITeacherCourseRepository teacherCourseRepository,
                                ISemesterCoursesRepository semesterCoursesRepository,
                                ICourseLetterScoreRepository courseLetterScoreRepository,
                                ILetterGradeRepository letterGradeRepository)
        {
            _mapper = mapper;
            _courseRepository = courseRepository;
            _teacherRepository = teacherRepository;
            _teacherCourseRepository = teacherCourseRepository;
            _semesterCoursesRepository = semesterCoursesRepository;
            _courseLetterScoreRepository = courseLetterScoreRepository;
            _letterGradeRepository = letterGradeRepository;
        }

        [HttpGet]
        public IActionResult Index(bool success, string message)
        {

            if (success)
            {
                ViewBag.Success = message;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CourseCreate(CourseCreateDto courseCreateDto)
        {
            var course = _mapper.Map<Course>(courseCreateDto);
            course = await _courseRepository.CreateAsync(course);
            return Json(new Response<Course>
            {
                Success = true,
                Message = "Kayıt Başarılı",
                Result = course
            });
        }

        [HttpPost]
        public async Task<IActionResult> CourseGetAll()
        {
            var data = await _courseRepository.GetAll(x => true).ToListAsync();
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id, bool success, string message)
        {
            var data = _teacherRepository.GetAll(x => true);
            ViewBag.Teacher = data;

            //derse bağlı öğretmenler geliyor
            ViewBag.TeacherCourse = await _teacherCourseRepository.GetTeacherCourseInnerJoinByCourseId(id) ?? new List<TeacherCourseDetailDto>();

            //dersin harf notu puan aralığını getiriyo , left join kullandık çünkü ilk tanımlandığında puan aralığı yok ise
            //sadece harf notunu getiriyoruz
            ViewBag.CourseLetterScore = (from lg in _letterGradeRepository.GetAll(x => true)
                                         join cls in _courseLetterScoreRepository.GetAll(x => x.CourseId == id) on lg.Id equals cls.LetterGradeId
                                         into clsGroup
                                         from cls in clsGroup.DefaultIfEmpty()
                                         select new CourseLetterScoreDetailDto
                                         {
                                             CourseId = id,
                                             LetterGradeId = lg.Id,
                                             CourseGrade = cls != null ? cls.CourseGrade : 0,
                                             LetterOrderBy = lg.OrderBy,
                                             Letter = lg.Letter,
                                         }).ToListAsync().Result.OrderBy(x => x.LetterOrderBy).ToList() ?? new List<CourseLetterScoreDetailDto>();

            var course = await _courseRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (success)
            {
                ViewBag.Success = message;
            }
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> CourseUpdate(CourseUpdateDto courseUpdateDto)
        {
            var course = await _courseRepository.FirstOrDefaultAsync(x => x.Id == courseUpdateDto.Id);
            if (course is not null)
            {
                _mapper.Map(courseUpdateDto, course);
                course = await _courseRepository.UpdateAsync(course);
                return Json(new Response<Course>
                {
                    Success = true,
                    Message = "Kayıt Başarılı.",
                    Result = course
                });
            }
            return Json(new Response<Course>
            {
                Success = false,
                Message = "Böyle bir ders mevcut değil.",
            });
        }

        [HttpPost]
        public async Task<IActionResult> TeacherCourseUpdate(List<TeacherCourseCreateDto> teacherCourseCreateDto)
        {
            var teacherCourse = _mapper.Map<List<TeacherCourse>>(teacherCourseCreateDto);
            if (teacherCourse.Count > 1)
            {
                //derse bağlı tüm hocaları siliyor
                await _teacherCourseRepository.DeleteAsync(x => x.CourseId == teacherCourse.First().CourseId);
                teacherCourse.RemoveAt(0);
                //sonra tekrar oluşturuyor
                await _teacherCourseRepository.ListCreateAsync(teacherCourse); 
                return Json(new Response<object>
                {
                    Success = true,
                    Message = "Kayıt Başarılı.",
                });
            }
            await _teacherCourseRepository.DeleteAsync(x => x.TeacherId == teacherCourse.First().TeacherId);
            return Json(new Response<object>
            {
                Success = true,
                Message = "Kayıt Başarılı.",
            });
        }

        [HttpPost]
        public async Task<IActionResult> CourseLetterScoreUpdate(CourseLetterScoreUpdateDto entity)
        {
            var letterCourse = await _courseLetterScoreRepository.FirstOrDefaultAsync(x => x.CourseId == entity.CourseId && x.LetterGradeId == entity.LetterGradeId);
            if (letterCourse is not null)
            {
                var courseLetterScore = _mapper.Map(entity, letterCourse);
                //derse bağlı tüm harf notlarını siliyor
                await _courseLetterScoreRepository.UpdateAsync(courseLetterScore);
            }
            else
            {
                var courseLetterScore = _mapper.Map<CourseLetterScore>(entity);
                await _courseLetterScoreRepository.CreateAsync(courseLetterScore);
            }
            return Json(new Response<object>
            {
                Success = true,
                Message = "Kayıt Başarılı.",
            });
        }


        [HttpPost]
        public async Task<IActionResult> CourseDelete(Guid Id)
        {
            var data = await _semesterCoursesRepository.GetAll(x => x.CourseId == Id).AnyAsync();
            if (data)
            {
                return Json(new Response<Course>
                {
                    Success = false,
                    Message = "Bu ders, bölümler tarafından kullanılmaktadır,silinemez."
                });
            }
            await _courseRepository.DeleteAsync(Id);
            return Json(new Response<Course>
            {
                Success = true,
                Message = "Silindi",
            });
        }
    }
}
