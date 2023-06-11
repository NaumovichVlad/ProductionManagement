namespace BusinessLogic.Dtos
{
    public class SalaryDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime PaymentDate { get; set; }
        public double Accrued { get; set; }
        public double ToBePaid { get; set; }
        public double Paid { get; set; }
        public EmployeeDto Employee { get; set; }
    }
}
