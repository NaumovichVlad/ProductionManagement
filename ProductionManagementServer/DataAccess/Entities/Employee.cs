using DataAccess.Interfaces;

namespace DataAccess.Entities
{
    public class Employee : IEntity
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string PassportNumber { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
