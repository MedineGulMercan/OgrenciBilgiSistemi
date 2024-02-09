using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemi.Const;
using OgrenciBilgiSistemi.Domain.DataBaseContext;
using OgrenciBilgiSistemi.Domain.Entities;
using OgrenciBilgiSistemi.Domain.IRepositories;
using OgrenciBilgiSistemi.Dto.User;

namespace OgrenciBilgiSistemi.Domain.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly Context _context;
        public UserRepository(Context context) : base(context)
        {
            _context = context;
        }
        public async Task<UserDetailDto> GetUserDetail(Guid id)
        {
            var data = await (from u in _context.Users
                              where u.Id == id
                              join r in _context.Roles on u.RoleId equals r.Id

                              join t in _context.Teachers on u.Id equals t.UserId into teachers
                              from teacher in teachers.DefaultIfEmpty()

                              join s in _context.Students on u.Id equals s.UserId into students
                              from student in students.DefaultIfEmpty()
                              select new UserDetailDto
                              {
                                  Id = r.RoleName == RoleConst.OgrenciRole ? student.Id : teacher.Id,
                                  UserId = u.Id,
                                  Surname = r.RoleName == RoleConst.OgrenciRole ? student.Surname : teacher.Surname,
                                  Name = r.RoleName == RoleConst.OgrenciRole ? student.Name : teacher.Name,
                                  Birthday = r.RoleName == RoleConst.OgrenciRole ? student.Birthday : teacher.Birthday,
                                  EmailAddress = r.RoleName == RoleConst.OgrenciRole ? student.EmailAddress : teacher.EmailAddress,
                                  PhoneNumber = r.RoleName == RoleConst.OgrenciRole ? student.PhoneNumber : teacher.PhoneNumber,
                                  RoleId = u.RoleId,
                                  RoleName = r.RoleName,
                                  DepartmentId = r.RoleName == RoleConst.OgrenciRole ? student.DepartmentId : null,
                                  TeacherId = r.RoleName == RoleConst.OgrenciRole ? null : teacher.Id,
                                  StudentId = r.RoleName == RoleConst.OgrenciRole ? student.Id : null,
                                  CityId = r.RoleName == RoleConst.OgrenciRole ? student.CityId : teacher.CityId,
                                  TC = r.RoleName == RoleConst.OgrenciRole ? student.TC : teacher.TC,
                                  ClassId = r.RoleName == RoleConst.OgrenciRole ? student.ClassId : null,
                              }).FirstOrDefaultAsync();

            return data;
        }
    }
}
