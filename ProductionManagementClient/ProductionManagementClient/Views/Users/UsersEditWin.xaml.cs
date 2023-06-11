using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Users;
using System.Windows;

namespace ProductionManagementClient.Views.Users
{
    /// <summary>
    /// Логика взаимодействия для UsersEditWin.xaml
    /// </summary>
    public partial class UsersEditWin : Window
    {
        public UsersEditWin(object id)
        {
            InitializeComponent();

            DataContext = new UsersEditViewModel(id.ToString(), new HttpApiClient(), new DialogService());
        }
    }
}
