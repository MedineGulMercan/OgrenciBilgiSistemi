namespace OgrenciBilgiSistemi.Dto.User
{
    public class UserDetailDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? TeacherId { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? CourseId { get; set; }
        public Guid? DepartmentId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string TC { get; set; }
        public string EmailAddress { get; set; }
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public Guid RoleId { get; set; }
        public Guid CityId { get; set; }
        public Guid? ClassId { get; set; }
        public string RoleName { get; set; }
    }
}
