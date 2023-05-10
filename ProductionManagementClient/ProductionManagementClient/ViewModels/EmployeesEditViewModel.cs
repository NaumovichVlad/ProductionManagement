using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProductionManagementClient.ViewModels
{
    public class EmployeesEditViewModel : ViewModelBase
    {
        private EmployeeModel _model;
        private IApiClient _client;

        public EmployeeModel Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        public EmployeesEditViewModel(string id, IApiClient client)
        {
            _client = client;
            Model = GetModel(id);
        }

        private RelayCommand _confirmCommand;
        public RelayCommand ConfirmCommand
        {
            get
            {
                return _confirmCommand ??
                    (_confirmCommand = new RelayCommand(obj =>
                    {
                        _client.Put(Model, "employee/edit");
                        ((Window)obj).Close();

                    }));
            }
        }

        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return _cancelCommand ??
                    (_cancelCommand = new RelayCommand(obj =>
                    {
                        ((Window)obj).Close();
                    }));
            }
        }

        public EmployeeModel GetModel(string id)
        {
            return _client.Get<EmployeeModel>($"employee/get/{id}").Result;
        }
    }
}
