
namespace OgrenciBilgiSistemi.Dto.User
{
    public class UserTableDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string TC { get; set; }
        public string EmailAddress { get; set; }
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
