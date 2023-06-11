using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Roles;
using System.Windows;

namespace ProductionManagementClient.Views.Roles
{
    /// <summary>
    /// Логика взаимодействия для RolesCreateWin.xaml
    /// </summary>
    public partial class RolesCreateWin : Window
    {
        public RolesCreateWin()
        {
            InitializeComponent();

            DataContext = new RolesCreateViewModel(new HttpApiClient(), new DialogService());
        }
    }
}
