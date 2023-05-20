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
