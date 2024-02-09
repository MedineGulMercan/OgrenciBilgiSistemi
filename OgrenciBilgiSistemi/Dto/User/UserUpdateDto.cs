namespace OgrenciBilgiSistemi.Dto.User
{
    public class UserUpdateDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string TC { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime Birthday { get; set; }
        public Guid? ClassId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid RoleId { get; set; }
        public Guid CityId { get; set; }
    }
}
