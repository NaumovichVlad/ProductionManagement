using ProductionManagementClient.Connection;
using ProductionManagementClient.Interfaces;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
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

namespace ProductionManagementClient.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IApiClient _client;
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
                        }
                    }));
                
            }
        }

        private UserModel GetUser()
        {
            return _client.Get<UserModel>($"login/user/{User.Login}").Result;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public LoginViewModel(IApiClient client)
        {
            _client = client;
            _user = new UserModel();
            _user.Role = "empty";
        }
    }
}
