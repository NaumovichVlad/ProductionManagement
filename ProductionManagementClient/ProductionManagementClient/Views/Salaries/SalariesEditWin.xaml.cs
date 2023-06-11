using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Salaries;
using System.Windows;

namespace ProductionManagementClient.Views.Salaries
{
    /// <summary>
    /// Логика взаимодействия для SalariesEditWin.xaml
    /// </summary>
    public partial class SalariesEditWin : Window
    {
        public SalariesEditWin(object id)
        {
            InitializeComponent();

            DataContext = new SalariesEditViewModel(id.ToString(), new HttpApiClient(), new DialogService());
        }
    }
}
