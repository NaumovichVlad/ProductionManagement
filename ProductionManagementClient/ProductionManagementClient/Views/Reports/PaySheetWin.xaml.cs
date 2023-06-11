using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Reports;
using System.Windows;

namespace ProductionManagementClient.Views.Reports
{
    /// <summary>
    /// Логика взаимодействия для PaySheetWin.xaml
    /// </summary>
    public partial class PaySheetWin : Window
    {
        public PaySheetWin()
        {
            InitializeComponent();

            DataContext = new PaySheetViewModel(new HttpApiClient(), new WordDocumentService(), new DialogService());
        }
    }
}
