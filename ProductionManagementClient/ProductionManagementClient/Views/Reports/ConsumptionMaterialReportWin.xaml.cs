using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Reports;
using System.Windows;

namespace ProductionManagementClient.Views.Reports
{
    /// <summary>
    /// Логика взаимодействия для ConsumptionMaterialReportWin.xaml
    /// </summary>
    public partial class ConsumptionMaterialReportWin : Window
    {
        public ConsumptionMaterialReportWin()
        {
            InitializeComponent();

            DataContext = new ConsumptionMaterialReportViewModel(new HttpApiClient(), new DialogService(), new WordDocumentService());
        }
    }
}
