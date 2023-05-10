using ProductionManagementClient.Connection;
using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;

namespace ProductionManagementClient.ViewModels
{
    public class EmployeesCreateViewModel : ViewModelBase
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

        public EmployeesCreateViewModel(IApiClient client)
        {
            _client = client;
            Model = new EmployeeModel();
        }

        private RelayCommand _confirmCommand;
        public RelayCommand ConfirmCommand
        {
            get
            {
                return _confirmCommand ??
                    (_confirmCommand = new RelayCommand(obj =>
                    {
                        _client.Post(Model, "employee/insert");
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
    }
}
