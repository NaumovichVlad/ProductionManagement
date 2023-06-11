using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Employees;
using System.Windows;

namespace ProductionManagementClient.Views
{
    /// <summary>
    /// Логика взаимодействия для EmployeesCreateWin.xaml
    /// </summary>
    public partial class EmployeesEditWin : Window
    {
        public EmployeesEditWin(object id)
        {
            InitializeComponent();

            DataContext = new EmployeesEditViewModel(id.ToString(), new HttpApiClient(), new DialogService());
        }
    }
}
