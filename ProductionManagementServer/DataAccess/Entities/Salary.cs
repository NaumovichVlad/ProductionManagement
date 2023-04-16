using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess.Entities
{
    public class Salary : IEntity
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime PaymentDate { get; set; }
        public double Accrued { get; set; }
        public double ToBePaid { get; set; }
        public double Paid { get; set; }
        public Employee Employee { get; set; }
    }
}
