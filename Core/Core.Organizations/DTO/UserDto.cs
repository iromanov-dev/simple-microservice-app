namespace Core.Organizations.DTO
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public long? OrganizationId { get; set; }
        public string? OrganizationName { get; set; }
    }
}
