using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Menus;
using System.Windows;

namespace ProductionManagementClient.Views.Menus
{
    /// <summary>
    /// Логика взаимодействия для PurchaseMainWin.xaml
    /// </summary>
    public partial class PurchaseMainWin : Window
    {
        public PurchaseMainWin()
        {
            InitializeComponent();

            DataContext = new PurchaseMainViewModel(new HttpApiClient(), new DialogService(), new WindowService());
        }
    }
}
