using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProductionManagementClient.ViewModels.Salaries
{
    public class SalariesCreateViewModel : CreateViewModel<SalaryModel>
    {
        private List<EmployeeModel> _employees;
        private EmployeeModel _selectedEmployee;

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

        public SalariesCreateViewModel(IApiClient client, IMessageBoxService messageBoxService) : base(client, messageBoxService)
        {
            Employees = _client.Get<List<EmployeeModel>>("employee/all").Result;
        }

        public override RelayCommand ConfirmCommand
        {
            get
            {
                return _confirmCommand ??
                    (_confirmCommand = new RelayCommand(obj =>
                    {
                        Model.EmployeeId = SelectedEmployee.Id;
                        Model.EmployeeSurname = SelectedEmployee.Surname;
                        Model.EmployeeName = SelectedEmployee.Name;
                        Model.EmployeeMiddleName = SelectedEmployee.MiddleName;

                        _client.Post(Model, "salary/insert");

                        _messageBoxService.ShowMessage("Добавление записи", "Запись успешно добавлена");
                        ((Window)obj).Close();

                    }));
            }
        }
    }
}
