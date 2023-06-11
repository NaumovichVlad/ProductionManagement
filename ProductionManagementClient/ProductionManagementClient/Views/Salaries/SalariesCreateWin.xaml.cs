using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Salaries;
using System.Windows;

namespace ProductionManagementClient.Views.Salaries
{
    /// <summary>
    /// Логика взаимодействия для SalariesCreateWin.xaml
    /// </summary>
    public partial class SalariesCreateWin : Window
    {
        public SalariesCreateWin()
        {
            InitializeComponent();

            DataContext = new SalariesCreateViewModel(new HttpApiClient(), new DialogService());
        }
    }
}
