using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OgrenciBilgiSistemi.Const;
using OgrenciBilgiSistemi.Domain.IRepositories;
using System.Security.Claims;

namespace OgrenciBilgiSistemi.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = RoleConst.OgretmenRole)]
    public class HomeController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;

        public HomeController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public IActionResult Index()
        {
            #region Teacher Id
            var teacherId = Guid.Empty;
            string teacherName = "";
            var userId = HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is not null)
            {
                var teacher = _teacherRepository.FirstOrDefaultAsync(x => x.UserId == Guid.Parse(userId)).Result;
                if (teacher is not null)
                    teacherId = teacher.Id;
                teacherName= teacher.Name + ' ' + teacher.Surname ;
            }
            #endregion
            ViewBag.TeacherName = teacherName;

            return View();
        }
    }
}
