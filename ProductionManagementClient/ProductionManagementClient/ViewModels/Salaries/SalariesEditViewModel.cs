using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ProductionManagementClient.ViewModels.Salaries
{
    public class SalariesEditViewModel : EditViewModel<SalaryModel>
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

        public SalariesEditViewModel(string id, IApiClient client, IDialogService messageBoxService) : base(id, client, messageBoxService)
        {
            Employees = _client.Get<List<EmployeeModel>>("employee/all").Result;

            SelectedEmployee = Employees.Where(e => e.Id == Model.EmployeeId).First();
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

                        _client.Put(Model, $"salary/edit");
                        _messageBoxService.ShowMessage("Изменение записи", "Запись успешно изменена");
                        ((Window)obj).Close();

                    }));
            }
        }

        public override SalaryModel GetModel(string id)
        {
            return _client.Get<SalaryModel>($"salary/get/{id}").Result;
        }
    }
}
