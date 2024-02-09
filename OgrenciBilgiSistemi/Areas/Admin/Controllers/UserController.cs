using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OgrenciBilgiSistemi.Areas.Admin.Models.User;
using OgrenciBilgiSistemi.Const;
using OgrenciBilgiSistemi.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using OgrenciBilgiSistemi.Dto;
using OgrenciBilgiSistemi.Dto.User;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.Repositories;
using OgrenciBilgiSistemi.Dto.TeacherCourse;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OgrenciBilgiSistemi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConst.AdminRole)]
    public class UserController : Controller
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;
        private readonly IClassRepository _classRepository;
        private readonly ITeacherCourseRepository _teacherCourseRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public UserController(IRoleRepository roleRepository,
                              ICityRepository cityRepository,
                              IUserRepository userRepository,
                              IStudentRepository studentRepository,
                              ITeacherRepository teacherRepository,
                              IMapper mapper,
                              IClassRepository classRepository,
                              ITeacherCourseRepository teacherCourseRepository,
                              ICourseRepository courseRepository,
                              IDepartmentRepository departmentRepository)
        {
            _roleRepository = roleRepository;
            _cityRepository = cityRepository;
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
            _mapper = mapper;
            _classRepository = classRepository;
            _teacherCourseRepository = teacherCourseRepository;
            _courseRepository = courseRepository;
            _departmentRepository = departmentRepository;
        }

        public IActionResult Index(bool success, string message)
        {
            ViewBag.Roles = _roleRepository.GetAll(x => true);
            ViewBag.City = _cityRepository.GetAll(x => true);

            ViewBag.Class = _classRepository.GetAll(x => true).ToListAsync().Result ?? new List<Class>();
            ViewBag.Department = _departmentRepository.GetAll(x => true).ToListAsync().Result ?? new List<Department>();

            if (success)
            {
                ViewBag.Success = message;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserCreate(UserCreateVM userCreateVM)
        {
            //son oluşturulan kullanıcının numarasını alıp +1 yaparak yeni numarasını yani kulalnıcı adını oluşturuyoruz.
            var role = await _roleRepository.FirstOrDefaultAsync(x => x.Id == userCreateVM.RoleId);
            var userName = await _userRepository.GetAll(x => true).OrderByDescending(x => x.UserName).FirstAsync();
            if (role is not null && role.RoleName == RoleConst.OgrenciRole)
            {
                var student = _mapper.Map<Domain.Entities.Student>(userCreateVM);
                student.User.UserName = (Convert.ToInt64(userName.UserName) + 1).ToString();
                await _studentRepository.CreateAsync(student);
            }
            else
            {
                var admin = _mapper.Map<Domain.Entities.Teacher>(userCreateVM);
                admin.User.UserName = (Convert.ToInt64(userName.UserName) + 1).ToString();
                await _teacherRepository.CreateAsync(admin);
            }

            return Json(new Response<object>
            {
                Success = true,
                Message = "Kayıt Başarılı.",
            });
        }

        [HttpPost]
        public async Task<IActionResult> UserGetAll()
        {
            var data = await (from u in _userRepository.GetAll(x => true)
                              join r in _roleRepository.GetAll(x => true) on u.RoleId equals r.Id

                              join t in _teacherRepository.GetAll(x => true) on u.Id equals t.UserId into teachers
                              from teacher in teachers.DefaultIfEmpty()

                              join s in _studentRepository.GetAll(x => true) on u.Id equals s.UserId into students
                              from student in students.DefaultIfEmpty()
                              select new UserTableDto
                              {
                                  Id = u.Id,
                                  Surname = r.RoleName == RoleConst.OgrenciRole ? student.Surname : teacher.Surname,
                                  Name = r.RoleName == RoleConst.OgrenciRole ? student.Name : teacher.Name,
                                  Birthday = r.RoleName == RoleConst.OgrenciRole ? student.Birthday : teacher.Birthday,
                                  EmailAddress = r.RoleName == RoleConst.OgrenciRole ? student.EmailAddress : teacher.EmailAddress,
                                  PhoneNumber = r.RoleName == RoleConst.OgrenciRole ? student.PhoneNumber : teacher.PhoneNumber,
                                  RoleId = u.RoleId,
                                  RoleName = r.RoleName,
                                  TC = r.RoleName == RoleConst.OgrenciRole ? student.TC : teacher.TC,
                              }).ToListAsync();
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id, bool success, string message)
        {
            ViewBag.Roles = _roleRepository.GetAll(x => true);
            ViewBag.City = _cityRepository.GetAll(x => true);

            var data = await _userRepository.GetUserDetail(id);

            if (RoleConst.OgretmenRole == data.RoleName)
            {
                ViewBag.Courses = await _courseRepository.GetAll(x => true).ToListAsync() ?? new List<Course>();

                ViewBag.TeacherCourse =  new List<TeacherCourseDetailDto>();
            }
            else if (RoleConst.OgrenciRole == data.RoleName)
            {
                ViewBag.Class = await _classRepository.GetAll(x => true).ToListAsync() ?? new List<Class>();
                ViewBag.Department = await _departmentRepository.GetAll(x => true).ToListAsync() ?? new List<Department>();
            }

            if (success)
            {
                ViewBag.Success = message;
            }
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> UserUpdate(UserUpdateDto userUpdateDto)
        {
            var user = await _userRepository.FirstOrDefaultAsync(x => x.Id == userUpdateDto.UserId);
            var role = await _roleRepository.FirstOrDefaultAsync(x => x.Id == user.RoleId);
            if (role is not null && role.RoleName == RoleConst.OgrenciRole)
            {
                var student = _mapper.Map<Domain.Entities.Student>(userUpdateDto);
                student.User.UserName = user.UserName;
                student.User.Password = user.Password;
                student.User.RoleId = user.RoleId;
                student.User.UserName = user.UserName;
                await _studentRepository.UpdateAsync(student);
            }
            else
            {
                var admin = _mapper.Map<Domain.Entities.Teacher>(userUpdateDto);
                admin.User.RoleId = user.RoleId;
                admin.User.UserName = user.UserName;
                admin.User.Password = user.Password;
                admin.User.UserName = user.UserName;
                await _teacherRepository.UpdateAsync(admin);
            }
            return Json(new Response<object>
            {
                Success = true,
                Message = "Kayıt Başarılı.",
            });
        }

   
    }
}
