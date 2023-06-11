using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Reports;
using System.Windows;

namespace ProductionManagementClient.Views.Reports
{
    /// <summary>
    /// Логика взаимодействия для DismissalOrderWin.xaml
    /// </summary>
    public partial class DismissalOrderWin : Window
    {
        public DismissalOrderWin()
        {
            InitializeComponent();

            DataContext = new DismissalOrderViewModel(new HttpApiClient(), new WordDocumentService(), new DialogService());
        }
    }
}
