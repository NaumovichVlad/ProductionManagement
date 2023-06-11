using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Menus;
using System.Windows;

namespace ProductionManagementClient.Views.Menus
{
    /// <summary>
    /// Логика взаимодействия для SaleMainWin.xaml
    /// </summary>
    public partial class SaleMainWin : Window
    {
        public SaleMainWin()
        {
            InitializeComponent();

            DataContext = new SaleMainViewModel(new HttpApiClient(), new DialogService(), new WindowService());
        }
    }
}
