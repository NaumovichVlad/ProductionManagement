namespace BusinessLogic.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public RoleDto Role { get; set; }
        public EmployeeDto Employee { get; set; }
    }
}
