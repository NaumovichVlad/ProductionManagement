using System;
using System.Collections.Generic;

namespace ProductionManagementClient.Models
{
    public class HiringOrderModel : ModelBase
    {
        private List<EmployeeModel> _employees;
        private EmployeeModel _selectedEmployee;
        private DateTime _hiringDate;
        private DateTime _compilationDate;
        private int _orderNumber;
        private string _note;

        public List<EmployeeModel> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                OnPropertyChanged();
            }
        }
        public EmployeeModel SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();
            }
        }
        public DateTime HiringDate
        {
            get => _hiringDate;
            set
            {
                _hiringDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime CompilationDate
        {
            get => _compilationDate;
            set
            {
                _compilationDate = value;
                OnPropertyChanged();
            }
        }
        public int OrderNumber
        {
            get => _orderNumber;
            set
            {
                _orderNumber = value;
                OnPropertyChanged();
            }
        }
        public string Note
        {
            get => _note;
            set
            {
                _note = value;
                OnPropertyChanged();
            }
        }
    }
}
