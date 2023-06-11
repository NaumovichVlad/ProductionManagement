using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Roles;
using System.Windows;

namespace ProductionManagementClient.Views.Roles
{
    /// <summary>
    /// Логика взаимодействия для RolesEditWin.xaml
    /// </summary>
    public partial class RolesEditWin : Window
    {
        public RolesEditWin(object id)
        {
            InitializeComponent();

            DataContext = new RolesEditViewModel(id.ToString(), new HttpApiClient(), new DialogService());
        }
    }
}
