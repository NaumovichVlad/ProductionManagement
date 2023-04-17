namespace API.Models
{
    public class SalaryModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime PaymentDate { get; set; }
        public double Accrued { get; set; }
        public double ToBePaid { get; set; }
        public double Paid { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string EmployeeMiddleName { get; set; }
    }
}
