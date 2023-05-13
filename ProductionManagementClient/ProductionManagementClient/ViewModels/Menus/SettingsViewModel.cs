using ProductionManagementClient.Connection;
using ProductionManagementClient.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProductionManagementClient.ViewModels.Menus
{
    public class SettingsViewModel : ViewModelBase
    {
        private string _uri;

        public string Uri
        {
            get
            {
                return _uri;
            }
            set
            {
                _uri = value;
                OnPropertyChanged();
            }
        }

        public SettingsViewModel()
        {
            Uri = GetServerUri();
        }

        private string GetServerUri()
        {
            return ApiConnection.GetConnectionString();
        }

        private void SetServerUri(string uri)
        {
            ApiConnection.SetConnectionString(uri);
        }

        private RelayCommand _setUriCommand;
        public RelayCommand SetUriCommand
        {
            get
            {
                return _setUriCommand ??
                    (_setUriCommand = new RelayCommand(obj =>
                    {
                        SetServerUri(Uri);
                        ((Window)obj).Close();
                    }));
            }
        }

        private RelayCommand _closeCommand;
        public RelayCommand CloseCommand
        {
            get
            {
                return _closeCommand ??
                    (_closeCommand = new RelayCommand(obj =>
                    {
                        ((Window)obj).Close();
                    }));
            }
        }
    }
}
