using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Reports;
using System.Windows;

namespace ProductionManagementClient.Views.Reports
{
    /// <summary>
    /// Логика взаимодействия для SalaryReportWin.xaml
    /// </summary>
    public partial class SalaryReportWin : Window
    {
        public SalaryReportWin()
        {
            InitializeComponent();

            DataContext = new SalaryReportViewModel(new WordDocumentService(), new DialogService(), new HttpApiClient());
        }
    }
}
