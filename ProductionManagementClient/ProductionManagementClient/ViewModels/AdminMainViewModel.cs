using ProductionManagementClient.Interfaces.Connection;
using ProductionManagementClient.Models;
using ProductionManagementClient.Services.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProductionManagementClient.ViewModels
{
    public class AdminMainViewModel : ViewModelBase
    {
        public ObservableCollection<ModelBase> Models { get; set; }
        private readonly IApiClient _client;

        public AdminMainViewModel(IApiClient client)
        {
            _client = client;
        }

        private ICommand _selectItemCommand;
        public ICommand SelectItemCommand
        {
            get
            {
                return _selectItemCommand ??
                    (_selectItemCommand = new RelayCommand(param =>
                    {
                        var item = (TreeViewItem)param;

                        switch (item.Name)
                        {
                            case "Roles":
                                Models = new ObservableCollection<ModelBase>(_client.Get<List<RoleModel>>("role/all").Result);
                                break;
                            case "Users":
                                break;
                            case "Employees":
                                break;
                        }
                    }
                    ));
            }
        }
    }
}
