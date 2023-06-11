using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Users;
using System.Windows;

namespace ProductionManagementClient.Views.Users
{
    /// <summary>
    /// Логика взаимодействия для UsersCreateWin.xaml
    /// </summary>
    public partial class UsersCreateWin : Window
    {
        public UsersCreateWin()
        {
            InitializeComponent();

            DataContext = new UsersCreateViewModel(new HttpApiClient(), new DialogService());
        }
    }
}
