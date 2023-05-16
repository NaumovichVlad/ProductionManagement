using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionManagementClient.Models
{
    public class SalaryModel : ModelBase
    {
        private int _id;
        private int _employeeId;
        private DateTime _paymentDate;
        private double _accrued;
        private double _toBePaid;
        private double _paid;
        private string _employeeSurname;
        private string _employeeName;
        private string _employeeMiddleName;
        public int Id 
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
        public int EmployeeId 
        {
            get => _employeeId;
            set
            {
                _employeeId = value;
                OnPropertyChanged();
            }
        }
        public DateTime PaymentDate 
        {
            get => _paymentDate;
            set
            {
                _paymentDate = value;
                OnPropertyChanged();
            }
        }
        public double Accrued 
        {
            get => _accrued;
            set
            {
                _accrued = value;
                OnPropertyChanged();
            }
        }
        public double ToBePaid 
        {
            get => _toBePaid;
            set
            {
                _toBePaid = value;
                OnPropertyChanged();
            }
        }
        public double Paid 
        {
            get => _paid;
            set
            {
                _paid = value;
                OnPropertyChanged();
            }
        }
        public string EmployeeName 
        {
            get => _employeeName;
            set
            {
                _employeeName = value;
                OnPropertyChanged();
            }
        }
        public string EmployeeSurname 
        {
            get => _employeeSurname;
            set
            {
                _employeeSurname = value;
                OnPropertyChanged();
            }
        }
        public string EmployeeMiddleName 
        {
            get => _employeeMiddleName;
            set
            {
                _employeeMiddleName = value;
                OnPropertyChanged();
            }
        }
    }
}
