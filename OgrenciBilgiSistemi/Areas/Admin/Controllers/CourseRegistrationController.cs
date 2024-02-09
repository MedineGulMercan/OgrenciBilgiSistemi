using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemi.Const;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;
using OgrenciBilgiSistemi.Domain.Repositories;
using OgrenciBilgiSistemi.Dto;
using OgrenciBilgiSistemi.Dto.CourseRegistration;
using OgrenciBilgiSistemi.Dto.CourseSelection;
using System.Data;

namespace OgrenciBilgiSistemi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConst.AdminRole)]
    public class CourseRegistrationController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICurrentSemesterRepository _currentSemesterRepository;
        private readonly ITeacherCourseRepository _teacherCourseRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISemesterRepository _semesterRepository;
        private readonly IMapper _mapper;
        private readonly ICourseSelectionRepository _courseSelectionRepository;
        private readonly ISemesterCoursesRepository _semesterCoursesRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public CourseRegistrationController(ICourseRepository courseRepository, IStudentRepository studentRepository, ICurrentSemesterRepository currentSemesterRepository, ITeacherCourseRepository teacherCourseRepository, ITeacherRepository teacherRepository, IClassRepository classRepository, ISemesterRepository semesterRepository, IMapper mapper, ICourseSelectionRepository courseSelectionRepository, ISemesterCoursesRepository semesterCoursesRepository, IDepartmentRepository departmentRepository)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _currentSemesterRepository = currentSemesterRepository;
            _teacherCourseRepository = teacherCourseRepository;
            _teacherRepository = teacherRepository;
            _classRepository = classRepository;
            _semesterRepository = semesterRepository;
            _mapper = mapper;
            _courseSelectionRepository = courseSelectionRepository;
            _semesterCoursesRepository = semesterCoursesRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<IActionResult> Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StudentGetAll()
        {
            var result = await (from cs in _courseSelectionRepository.GetAll(x => !x.IsRegistrationConfirmed)
                                join s in _studentRepository.GetAll(x => true) on cs.StudentId equals s.Id
                                select new StudentDto
                                {
                                    StudentId = cs.StudentId,
                                    StudentName = s.Name + ' ' + s.Surname,

                                }).Distinct().ToListAsync();
            return Json(result);
        }

        [HttpGet]
        //herhangi bir öğrenci ders kaydı yaptıysa ders kaydı verilerini çekiyoruz.
        public async Task<IActionResult> Detail(Guid Id, bool success, string message)
        {
            var semester = await _currentSemesterRepository.FirstOrDefaultAsync(x => true);
            var student = await _studentRepository.FirstOrDefaultAsync(x => x.Id == Id);

            var data = await (from cs in _courseSelectionRepository.GetAll(x => x.StudentId == Id)
                              join c in _semesterCoursesRepository.GetAll(x => true) on cs.SemesterCoursesId equals c.Id
                              join co in _courseRepository.GetAll(x => true) on c.CourseId equals co.Id
                              join d in _departmentRepository.GetAll(x => true) on c.DepartmentId equals d.Id
                              join t in _teacherRepository.GetAll(x => true) on c.TeacherId equals t.Id
                              join cl in _classRepository.GetAll(x => true) on c.ClassId equals cl.Id
                              join sm in _semesterRepository.GetAll(x => true) on semester.SemesterId equals sm.Id
                              select new CourseSelectionDto
                              {
                                  StudentId = Id,
                                  StudentName = student.Name + ' ' + student.Surname,
                                  ClassId = c.ClassId,
                                  ClassName = cl.ClassName,
                                  CourseId = c.CourseId,
                                  CourseName = co.CourseName,
                                  DepartmentId = c.DepartmentId,
                                  DepartmentName = d.DepartmentName,
                                  TeacherId = c.TeacherId,
                                  TeacherNameSurname = t.Name + ' ' + t.Surname,
                                  CurrentSemesterId = semester.SemesterId,
                                  CurrentSemesterName = sm.SemesterName,
                              }).ToListAsync();
            ViewBag.CourseRegistration = data;
            return View();
        }

        //Ders kaydını onaylama işlemini yapıyoruz
        [HttpPost]
        public async Task<IActionResult> RegistrationApproved(Guid id)
        {
            var approv = await _courseSelectionRepository.GetAll(x => x.StudentId == id).ToListAsync();
            foreach (var item in approv)
            {
                item.IsRegistrationConfirmed = true;
                await _courseSelectionRepository.UpdateAsync(item);

            }
            return Json(new Response<CourseSelection>
            {
                Success = true,
                Message = "Kayıt Başarılı.",
            });
        }

        //Ders kaydını reddetme işlemini yapıyoruz, direkt kaydı db den siliyorum
        [HttpPost]
        public async Task<IActionResult> DeleteRegister(Guid id)
        {
            var approv = await _courseSelectionRepository.GetAll(x => x.StudentId == id).ToListAsync();
            foreach (var item in approv)
            {
                await _courseSelectionRepository.DeleteAsync(item);

            }
            return Json(new Response<CourseSelection>
            {
                Success = true,
                Message = "Kayıt Başarılı.",
            });
        }







    }


}
