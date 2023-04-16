using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogic.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string PassportNumber { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }
    }
}
