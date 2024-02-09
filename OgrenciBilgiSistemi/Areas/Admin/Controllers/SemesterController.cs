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
using OgrenciBilgiSistemi.Dto.Semester;

namespace OgrenciBilgiSistemi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConst.AdminRole)]
    public class SemesterController : Controller
    {
        private readonly ISemesterRepository _semesterRepository;
        private readonly IMapper _mapper;
        private readonly ISemesterCoursesRepository _semesterCoursesRepository;

        public SemesterController(ISemesterRepository semesterRepository,
                                  IMapper mapper,
                                  ISemesterCoursesRepository semesterCoursesRepository)
        {
            _semesterRepository = semesterRepository;
            _mapper = mapper;
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
        public async Task<IActionResult> SemesterCreate(SemesterCreateDto semesterCreateDto)
        {
            var semester = _mapper.Map<Semester>(semesterCreateDto);
            semester = await _semesterRepository.CreateAsync(semester);
            return Json(new Response<Semester>
            {
                Success = true,
                Message = "Kayıt Başarılı",
                Result = semester
            });
        }

        [HttpPost]
        public async Task<IActionResult> SemesterGetAll()
        {
            var data = await _semesterRepository.GetAll(x => true).ToListAsync();
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id, bool success, string message)
        {
            var semester = await _semesterRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (success)
            {
                ViewBag.Success = message;
            }
            return View(semester);
        }

        [HttpPost]
        public async Task<IActionResult> SemesterUpdate(SemesterUpdateDto semesterUpdateDto)
        {
            var semester = await _semesterRepository.FirstOrDefaultAsync(x => x.Id == semesterUpdateDto.Id);
            if (semester is not null)
            {
                _mapper.Map(semesterUpdateDto, semester);
                semester = await _semesterRepository.UpdateAsync(semester);
                return Json(new Response<Semester>
                {
                    Success = true,
                    Message = "Kayıt Başarılı.",
                    Result = semester
                });
            }
            return Json(new Response<Class>
            {
                Success = false,
                Message = "Böyle bir semester mevcut değil.",
            });
        }

        [HttpPost]
        public async Task<IActionResult> SemesterDelete(Guid Id)
        {
            var data = await _semesterCoursesRepository.GetAll(x => x.SemesterId == Id).AnyAsync();
            if (data)
            {
                return Json(new Response<Semester>
                {
                    Success = false,
                    Message = "Bu dönem, bölümler tarafından kullanılmaktadır,silinemez."
                });
            }
            await _semesterRepository.DeleteAsync(Id);
            return Json(new Response<Semester>
            {
                Success = true,
                Message = "Silindi",
            });
        }
    }
}
