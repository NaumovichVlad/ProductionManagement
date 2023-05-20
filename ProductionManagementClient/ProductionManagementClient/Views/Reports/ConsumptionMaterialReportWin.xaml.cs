using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
