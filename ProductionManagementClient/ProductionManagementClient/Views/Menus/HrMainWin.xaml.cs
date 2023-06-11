using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Menus;
using System.Windows;

namespace ProductionManagementClient.Views.Menus
{
    /// <summary>
    /// Логика взаимодействия для HrMainWin.xaml
    /// </summary>
    public partial class HrMainWin : Window
    {
        public HrMainWin()
        {
            InitializeComponent();

            DataContext = new HrMainViewModel(new HttpApiClient(), new WindowService());
        }
    }
}
