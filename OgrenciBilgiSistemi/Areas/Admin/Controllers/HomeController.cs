using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OgrenciBilgiSistemi.Const;
using OgrenciBilgiSistemi.Domain.IRepositories;
using System.Security.Claims;

namespace OgrenciBilgiSistemi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConst.AdminRole)]
    public class HomeController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;

        public HomeController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public IActionResult Index()
        {
            #region admin Id
            var adminId = Guid.Empty;
            string adminName = "";
            var userId = HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is not null)
            {
                var admin = _teacherRepository.FirstOrDefaultAsync(x => x.UserId == Guid.Parse(userId)).Result;
                if (admin is not null)
                    adminId=admin.Id;
                    adminName = admin.Name + ' ' + admin.Surname;
            }
            #endregion
            ViewBag.AdminName = adminName;
            return View();
        }
    }
}
