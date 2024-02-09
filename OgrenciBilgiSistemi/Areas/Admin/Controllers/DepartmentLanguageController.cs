using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemi.Const;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;
using OgrenciBilgiSistemi.Domain.Repositories;
using OgrenciBilgiSistemi.Dto;
using OgrenciBilgiSistemi.Dto.DepartmentLanguage;

namespace OgrenciBilgiSistemi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConst.AdminRole)]
    public class DepartmentLanguageController : Controller
    {
        private readonly IDepartmentLanguageRepository _departmentLanguageRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentLanguageController(IDepartmentLanguageRepository departmentLanguageRepository, IMapper mapper, IDepartmentRepository departmentRepository)
        {
            _departmentLanguageRepository = departmentLanguageRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
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
        public async Task<IActionResult> DepartmentLanguageCreate(DepartmentLanguageCreateDto departmentLanguagedto)
        {
            //mapleme : entity içindeki verileri tek tek atamak yerine bu işlemi map ile otomatik yaptırıyoruz. 
            var language = _mapper.Map<DepartmentLanguage>(departmentLanguagedto);
            try
            {
                language = await _departmentLanguageRepository.CreateAsync(language);
            }
            catch (Exception)
            {

                throw;
            }
            return Json(new Response<DepartmentLanguage>
            {
                Success = true,
                Message = "Kayıt Başarılı",
                Result = language
            });
        }
        [HttpPost]
        public async Task<IActionResult> DepartmentLanguageGetAll()
        {
            var data = await _departmentLanguageRepository.GetAll(x => true).ToListAsync();
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid Id, bool success, string message)
        {
            var data = await _departmentLanguageRepository.FirstOrDefaultAsync(x => x.Id == Id);
            if (success)
            {
                ViewBag.Success = message;
            }
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> DepartmentLanguageUpdate(DepartmentLanguageUpdateDto departmentLanguageUpdateDto)
        {
            var language = await _departmentLanguageRepository.FirstOrDefaultAsync(x => x.Id == departmentLanguageUpdateDto.Id);

            if (language is not null)
            {
                _mapper.Map(departmentLanguageUpdateDto, language);

                language = await _departmentLanguageRepository.UpdateAsync(language);

                return Json(new Response<DepartmentLanguage>
                {
                    Success = true,
                    Message = "Kayıt Başarılı.",
                    Result = language,
                });
            }
            return Json(new Response<DepartmentLanguage>
            {
                Success = false,
                Message = "Belirtilen ID'ye sahip dil bulunamadı.",
            });
        }
        [HttpPost]
        public async Task<IActionResult> DepartmentLanguageDelete(Guid Id)
        {
            var data = await _departmentRepository.GetAll(x => x.DepartmentLanguageId == Id).AnyAsync();
            if(data)
            {
                //response js tarafına göndermek için oluşturduğumuz dinamik bi sınıf
                return Json(new Response<DepartmentLanguage>
                {
                    Success=false,
                    Message="Bu dil, bölümler tarafından kullanılmaktadır,silinemez."
                });
            }
            await _departmentLanguageRepository.DeleteAsync(Id);
            return Json(new Response<DepartmentLanguage>
            {
                Success = true,
                Message = "Silindi",
            });
        }
    }
}
