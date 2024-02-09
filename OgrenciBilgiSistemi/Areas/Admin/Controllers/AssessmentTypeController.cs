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
using OgrenciBilgiSistemi.Dto.Semester;

namespace OgrenciBilgiSistemi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AssessmentTypeController : Controller
    {

        private readonly IAssessmentTypeRepository _assessmentTypeRepository;
        private readonly IMapper _mapper;
        private readonly ICourseAssessmentRepository _courseAssessmentRepository;
        public AssessmentTypeController(IAssessmentTypeRepository assessmentTypeRepository, IMapper mapper, ICourseAssessmentRepository courseAssessmentRepository)
        {
            _assessmentTypeRepository = assessmentTypeRepository;
            _mapper = mapper;
            _courseAssessmentRepository = courseAssessmentRepository;
        }

        [Authorize(Roles = RoleConst.AdminRole)]
        [HttpGet]
        public IActionResult Index(bool success, string message)
        {

            if (success)
            {
                ViewBag.Success = message;
            }
            return View();
        }
        [Authorize(Roles = RoleConst.AdminRole)]
        [HttpPost]
        public async Task<IActionResult> AssessmentTypeCreate(AssessmentTypeCreateDto assessmentTypeCreateDto)
        {
            var assessmentType = _mapper.Map<AssessmentType>(assessmentTypeCreateDto);
            assessmentType = await _assessmentTypeRepository.CreateAsync(assessmentType);
            return Json(new Response<AssessmentType>
            {
                Success = true,
                Message = "Kayıt Başarılı",
                Result = assessmentType
            });
        }
        [Authorize(Roles = RoleConst.AdminAndOgretmenRole)]
        [HttpPost]
        public async Task<IActionResult> AssessmentTypeGetAll()
        {
            var data = await _assessmentTypeRepository.GetAll(x => true).ToListAsync();
            return Json(data);
        }
        [Authorize(Roles = RoleConst.AdminRole)]
        [HttpGet]
        public async Task<IActionResult> Detail(Guid id, bool success, string message)
        {
            var semester = await _assessmentTypeRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (success)
            {
                ViewBag.Success = message;
            }
            return View(semester);
        }

        [Authorize(Roles = RoleConst.AdminRole)]
        [HttpPost]
        public async Task<IActionResult> AssessmentTypeUpdate(AssessmentTypeUpdateDto assessmentTypeUpdateDto)
        {
            var assessmentType = await _assessmentTypeRepository.FirstOrDefaultAsync(x => x.Id == assessmentTypeUpdateDto.Id);
            if (assessmentType is not null)
            {
                _mapper.Map(assessmentTypeUpdateDto, assessmentType);
                assessmentType = await _assessmentTypeRepository.UpdateAsync(assessmentType);
                return Json(new Response<AssessmentType>
                {
                    Success = true,
                    Message = "Güncelleme Başarılı.",
                    Result = assessmentType
                });
            }
            return Json(new Response<Class>
            {
                Success = false,
                Message = "Böyle bir sınav türü mevcut değil.",
            });
        }
        [HttpPost]
        public async Task<IActionResult> AssessmentTypeDelete(Guid Id)
        {
            var data = await _courseAssessmentRepository.GetAll(x => x.AssessmentTypeId == Id).AnyAsync();
            if (data)
            {
                return Json(new Response<Class>
                {
                    Success = false,
                    Message = "Bu sınav türü, sınavlar tarafından kullanılmaktadır,silinemez."
                });
            }
            await _assessmentTypeRepository.DeleteAsync(Id);
            return Json(new Response<AssessmentType>
            {
                Success = true,
                Message = "Silindi",
            });
        }
    }
}
