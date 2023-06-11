using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Reports;
using System.Windows;

namespace ProductionManagementClient.Views.Reports
{
    /// <summary>
    /// Логика взаимодействия для HiringOrderWin.xaml
    /// </summary>
    public partial class HiringOrderWin : Window
    {
        public HiringOrderWin()
        {
            InitializeComponent();

            DataContext = new HiringOrderViewModel(new HttpApiClient(), new WordDocumentService(), new DialogService());
        }
    }
}
