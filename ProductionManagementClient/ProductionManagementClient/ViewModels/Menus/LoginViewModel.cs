using ProductionManagementClient.Connection;
using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Interfaces.Services;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using ProductionManagementClient.Views;
using ProductionManagementClient.Views.Menus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;

namespace ProductionManagementClient.ViewModels.Menus
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IApiClient _client;
        private readonly IWindowService _windowService;
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
                        var responce = _client.Post(User, "login/autorize").Result;

                        if (responce.StatusCode == HttpStatusCode.OK)
                        {
                            ApiConnection.Token = JsonNode.Parse(responce.Content.ReadAsStringAsync().Result)["token"].ToString();
                            ApiConnection.User = GetUser();
                            OpenMainWindow();
                            ((Window)obj).Close();
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
            }
        }

        public LoginViewModel(IApiClient client, IWindowService windowService)
        {
            _client = client;
            _windowService = windowService;
            _user = new UserModel();
            _user.RoleName = "empty";
            _user.EmployeeSurname = "empty";
            _user.EmployeeName = "empty";
            _user.EmployeeMiddleName = "empty";
        }
    }
}
