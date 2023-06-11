using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Menus;
using System.Windows;

namespace ProductionManagementClient.Views.Menus
{
    /// <summary>
    /// Логика взаимодействия для AdminMainWin.xaml
    /// </summary>
    public partial class AdminMainWin : Window
    {
        public AdminMainWin()
        {
            InitializeComponent();
            DataContext = new AdminMainViewModel(new HttpApiClient(), new WindowService(), new DialogService());
        }
    }
}
