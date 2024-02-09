using Dto.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using OgrenciBilgiSistemi.Domain.IRepositories;
using System.Net;
using System.Security.Claims;

namespace OgrenciBilgiSistemi.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IStudentRepository _studentRepository;
        public LoginController(IUserRepository userRepository,
                               IRoleRepository roleRepository,
                               ITeacherRepository teacherRepository,
                               IStudentRepository studentRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _teacherRepository = teacherRepository;
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginDto loginDto)
        {
            //giriş yapan kullanıcının verisini çekiyoruz ve rolünü çekiyoruz.
            var loginUserData = await _userRepository.FirstOrDefaultAsync(x => x.Password == loginDto.Password && x.UserName == loginDto.TC);
            if (loginUserData is not null)
            {
                var role = await _roleRepository.FirstOrDefaultAsync(x => x.Id == loginUserData.RoleId);
                //claim'in name'ine giriş yapan kullanıcının tc'sini veriyoruz. Claim : Giriş yapan kullanıcının bilgileriyle token oluşturuyor.
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginUserData.UserName),
                    new Claim(ClaimTypes.Role, role.RoleName),
                    new Claim(ClaimTypes.NameIdentifier,loginUserData.Id.ToString()),//claimin içine kullanıcının id sini kaydettik, artık istediğimiz her yerden erişebiliriz.
                };
                var userIdentity = new ClaimsIdentity(claims, "Login");
                var principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal); //burada tokenı oluşturuyor.

                return Json(new { Authorization = true, Type = role.RoleName }); //
            }
            else
            {
                return Json(false);
            }
        }
        //Tokenı siliyor. Başka sayfaya geçtiğimizde tokenı sildiği için bizi sistemden atıyor giriş yapmanı istiyor. 
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("SignIn","Login");
        }
        [HttpGet]
        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }
    }
}
