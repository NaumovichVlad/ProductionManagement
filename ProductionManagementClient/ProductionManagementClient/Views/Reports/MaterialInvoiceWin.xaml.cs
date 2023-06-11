using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Reports;
using System.Windows;

namespace ProductionManagementClient.Views.Reports
{
    /// <summary>
    /// Логика взаимодействия для MaterialInvoiceWin.xaml
    /// </summary>
    public partial class MaterialInvoiceWin : Window
    {
        public MaterialInvoiceWin(object id)
        {
            InitializeComponent();

            DataContext = new MaterialInvoiceViewModel(int.Parse(id.ToString()), new WordDocumentService(), new DialogService(), new HttpApiClient());
        }
    }
}
