using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Employees;
using System.Windows;

namespace ProductionManagementClient.Views
{
    /// <summary>
    /// Логика взаимодействия для EmployeesCreateWin.xaml
    /// </summary>
    public partial class EmployeesCreateWin : Window
    {
        public EmployeesCreateWin()
        {
            InitializeComponent();

            DataContext = new EmployeesCreateViewModel(new HttpApiClient(), new DialogService());
        }
    }
}
