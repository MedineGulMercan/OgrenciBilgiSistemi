using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemi.Const;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;
using OgrenciBilgiSistemi.Domain.Repositories;
using OgrenciBilgiSistemi.Dto;
using OgrenciBilgiSistemi.Dto.Class;
using OgrenciBilgiSistemi.Dto.Course;

namespace OgrenciBilgiSistemi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConst.AdminRole)]
    public class ClassController : Controller
    {
        private readonly IClassRepository _classRepository;
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ISemesterCoursesRepository _semesterCoursesRepository;
        public ClassController(IClassRepository classRepository, IMapper mapper, IDepartmentRepository departmentRepository, ISemesterCoursesRepository semesterCoursesRepository)
        {
            _classRepository = classRepository;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
            _semesterCoursesRepository = semesterCoursesRepository;
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
        public async Task<IActionResult> ClassCreate(ClassCreateDto classCreateDto)
        {
            var data = _mapper.Map<Class>(classCreateDto);
            data = await _classRepository.CreateAsync(data);
            return Json(new Response<Class>
            {
                Success = true,
                Message = "Kayıt Başarılı",
                Result = data
            });
        }

        [HttpPost]
        public async Task<IActionResult> ClassGetAll()
        {
            var data = await _classRepository.GetAll(x => true).ToListAsync();
            return Json(data);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(Guid id, bool success, string message)
        {
            var course = await _classRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (success)
            {
                ViewBag.Success = message;
            }
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> ClassUpdate(ClassUpdateDto classUpdateDto)
        {
            var classData = await _classRepository.FirstOrDefaultAsync(x => x.Id == classUpdateDto.Id);
            if (classData is not null)
            {
                _mapper.Map(classUpdateDto, classData);
                classData = await _classRepository.UpdateAsync(classData);
                return Json(new Response<Class>
                {
                    Success = true,
                    Message = "Kayıt Başarılı.",
                    Result = classData
                });
            }
            return Json(new Response<Class>
            {
                Success = false,
                Message = "Böyle bir ders mevcut değil.",
            });
        }
        [HttpPost]
        public async Task<IActionResult> ClassDelete(Guid Id)
        {
            var data = await _semesterCoursesRepository.GetAll(x => x.ClassId == Id).AnyAsync();
            if (data)
            {
                return Json(new Response<Class>
                {
                    Success = false,
                    Message = "Bu sınıf, bölümler tarafından kullanılmaktadır,silinemez."
                });
            }
            await _classRepository.DeleteAsync(Id);
            return Json(new Response<DepartmentLanguage>
            {
                Success = true,
                Message = "Silindi",
            });
        }


    }
}
