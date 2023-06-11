using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Reports;
using System.Windows;

namespace ProductionManagementClient.Views.Reports
{
    /// <summary>
    /// Логика взаимодействия для AvailableMaterialsReportWin.xaml
    /// </summary>
    public partial class AvailableMaterialsReportWin : Window
    {
        public AvailableMaterialsReportWin()
        {
            InitializeComponent();

            DataContext = new AvailableMaterialReportViewModel(new HttpApiClient(), new DialogService(), new WordDocumentService());
        }
    }
}
