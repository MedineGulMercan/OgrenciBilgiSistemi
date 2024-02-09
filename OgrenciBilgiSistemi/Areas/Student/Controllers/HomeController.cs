using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OgrenciBilgiSistemi.Const;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;
using OgrenciBilgiSistemi.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace OgrenciBilgiSistemi.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = RoleConst.OgrenciRole)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStudentRepository _studentRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IClassRepository _classRepository;
        private readonly IUserRepository _userRepository;

        public HomeController(ILogger<HomeController> logger, IStudentRepository studentRepository, IDepartmentRepository departmentRepository, IClassRepository classRepository, IUserRepository userRepository)
        {
            _logger = logger;
            _studentRepository = studentRepository;
            _departmentRepository = departmentRepository;
            _classRepository = classRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            #region student Id
            var studentId = Guid.Empty;
            string studentName ="";
            string departmentName = "";
            var className = "";
            var studentNumber = "";
            var userId = HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is not null)
            {
                var student = _studentRepository.FirstOrDefaultAsync(x => x.UserId == Guid.Parse(userId)).Result;
               var department = _departmentRepository.FirstOrDefaultAsync(x => x.Id == student.DepartmentId).Result;
                var classs = _classRepository.FirstOrDefaultAsync(x => x.Id == student.ClassId).Result;
                var number = _userRepository.FirstOrDefaultAsync(x => x.Id == student.UserId).Result;

                if (student is not null)
                    studentId = student.Id;
                studentName = student.Name + ' ' + student.Surname;
                departmentName = department.DepartmentName;
                className = classs.ClassName;
                studentNumber = number.UserName;
            }
            #endregion
            ViewBag.StudentName = studentName;
            ViewBag.DepartmentName = departmentName;
            ViewBag.ClassName = className;
            ViewBag.StudentNumber = studentNumber;
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}