using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Menus;
using System.Windows;

namespace ProductionManagementClient.Views.Menus
{
    /// <summary>
    /// Логика взаимодействия для ManufacturerMainWin.xaml
    /// </summary>
    public partial class ManufacturerMainWin : Window
    {
        public ManufacturerMainWin()
        {
            InitializeComponent();

            DataContext = new ManufacturerMainViewModel(new HttpApiClient(), new WindowService(), new DialogService());
        }
    }
}
