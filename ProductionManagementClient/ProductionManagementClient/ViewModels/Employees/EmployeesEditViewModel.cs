﻿using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using System.Windows;

namespace ProductionManagementClient.ViewModels.Employees
{
    public class EmployeesEditViewModel : EditViewModel<EmployeeModel>
    {
        public EmployeesEditViewModel(string id, IApiClient client, IDialogService messageBoxService)
            : base(id, client, messageBoxService)
        { }

        public override RelayCommand ConfirmCommand
        {
            get
            {
                return _confirmCommand ??
                    (_confirmCommand = new RelayCommand(obj =>
                    {
                        _client.Put(Model, $"employee/edit");
                        _messageBoxService.ShowMessage("Изменение записи", "Запись успешно изменена");
                        ((Window)obj).Close();

                    }));
            }
        }

        public override EmployeeModel GetModel(string id)
        {
            return _client.Get<EmployeeModel>($"employee/get/{id}").Result;
        }
    }
}
