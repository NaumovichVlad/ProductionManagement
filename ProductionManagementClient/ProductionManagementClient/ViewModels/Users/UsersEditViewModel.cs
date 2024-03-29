﻿using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ProductionManagementClient.ViewModels.Users
{
    public class UsersEditViewModel : EditViewModel<UserModel>
    {
        private List<EmployeeModel> _employees;
        private List<RoleModel> _roles;
        private EmployeeModel _selectedEmployee;
        private RoleModel _selectedRole;

        public List<EmployeeModel> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                OnPropertyChanged();
            }
        }
        public List<RoleModel> Roles
        {
            get => _roles;
            set
            {
                _roles = value;
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
        public RoleModel SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                OnPropertyChanged();
            }
        }
        public UsersEditViewModel(string id, IApiClient client, IDialogService messageBoxService)
            : base(id, client, messageBoxService)
        {
            Employees = _client.Get<List<EmployeeModel>>("employee/all").Result;
            Roles = _client.Get<List<RoleModel>>("role/all").Result;

            SelectedEmployee = Employees.Where(e => e.Id == Model.EmployeeId).First();
            SelectedRole = Roles.Where(r => r.Id == Model.RoleId).First();
        }

        public override RelayCommand ConfirmCommand
        {
            get
            {
                return _confirmCommand ??
                    (_confirmCommand = new RelayCommand(obj =>
                    {
                        Model.RoleId = SelectedRole.Id;
                        Model.EmployeeId = SelectedEmployee.Id;
                        Model.EmployeeSurname = SelectedEmployee.Surname;
                        Model.EmployeeName = SelectedEmployee.Name;
                        Model.EmployeeMiddleName = SelectedEmployee.MiddleName;
                        Model.RoleName = SelectedRole.Name;

                        _client.Put(Model, $"user/edit");
                        _messageBoxService.ShowMessage("Изменение записи", "Запись успешно изменена");
                        ((Window)obj).Close();

                    }));
            }
        }

        public override UserModel GetModel(string id)
        {
            return _client.Get<UserModel>($"user/get/{id}").Result;
        }
    }
}
