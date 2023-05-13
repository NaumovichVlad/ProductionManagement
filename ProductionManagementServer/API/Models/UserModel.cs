using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class UserModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public string EmployeeSurname { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string EmployeeMiddleName { get; set; }

    }
}
