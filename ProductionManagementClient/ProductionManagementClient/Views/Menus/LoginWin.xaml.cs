using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Menus;
using System.Windows;

namespace ProductionManagementClient.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWin : Window
    {
        public LoginWin()
        {
            InitializeComponent();

            DataContext = new LoginViewModel(new HttpApiClient(), new WindowService(), new DialogService());
        }
    }
}
