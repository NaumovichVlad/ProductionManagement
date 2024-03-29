﻿using ProductionManagementClient.Connection;
using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using ProductionManagementClient.Views;
using ProductionManagementClient.Views.Menus;
using System;
using System.Net;
using System.Text.Json.Nodes;
using System.Windows;

namespace ProductionManagementClient.ViewModels.Menus
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IApiClient _client;
        private readonly IWindowService _windowService;
        private readonly IDialogService _dialogService;
        private UserModel _user;
        private RelayCommand _loginCommand;
        public UserModel User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }
        }


        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ??
                    (_loginCommand = new RelayCommand(obj =>
                    {
                        try
                        {
                            var responce = _client.Post(User, "login/autorize").Result;

                            if (responce.StatusCode == HttpStatusCode.OK)
                            {
                                ApiConnection.Token = JsonNode.Parse(responce.Content.ReadAsStringAsync().Result)["token"].ToString();
                                ApiConnection.User = GetUser();
                                OpenMainWindow();
                                ((Window)obj).Close();
                            }
                            else
                            {
                                _dialogService.ShowErrorMessage("Ошибка авторизации", "Логин или пароль введён неверно");
                            }
                        }
                        catch (AggregateException ex)
                        {
                            _dialogService.ShowErrorMessage("Ошибка подключения", "Подключение к серверу не установлено");
                        }
                    }));

            }
        }

        private RelayCommand _settingsCommand;

        public RelayCommand SettingsCommand
        {
            get
            {
                return _settingsCommand ??
                    (_settingsCommand = new RelayCommand(obj =>
                    {
                        _windowService.ShowWindow<SettingsWin>();
                    }));

            }
        }

        private UserModel GetUser()
        {
            return _client.Get<UserModel>($"login/user/{User.Login}").Result;
        }

        private void OpenMainWindow()
        {
            switch (ApiConnection.User.RoleName)
            {
                case "admin":
                    _windowService.ShowWindow<AdminMainWin>();
                    break;
                case "hr":
                    _windowService.ShowWindow<HrMainWin>();
                    break;
                case "logistics":
                    _windowService.ShowWindow<LogisticMainWin>();
                    break;
                case "purchases":
                    _windowService.ShowWindow<PurchaseMainWin>();
                    break;
                case "sales":
                    _windowService.ShowWindow<SaleMainWin>();
                    break;
                case "manufacturer":
                    _windowService.ShowWindow<ManufacturerMainWin>();
                    break;
            }
        }

        public LoginViewModel(IApiClient client, IWindowService windowService, IDialogService dialogService)
        {
            _client = client;
            _windowService = windowService;
            _user = new UserModel();
            _user.RoleName = "empty";
            _user.EmployeeSurname = "empty";
            _user.EmployeeName = "empty";
            _user.EmployeeMiddleName = "empty";
            _dialogService = dialogService;
        }
    }
}
