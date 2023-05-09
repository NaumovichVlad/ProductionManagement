using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogic.Dtos
{
    public class CounteragentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int INN { get; set; }
        public int AccountNumber { get; set; }
        public string Address { get; set; }
        public int PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string RegistrationCountry { get; set; }
    }
}
