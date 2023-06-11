using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Products;
using System.Windows;

namespace ProductionManagementClient.Views.Products
{
    /// <summary>
    /// Логика взаимодействия для FinishedProductsSalesEditWin.xaml
    /// </summary>
    public partial class FinishedProductsSalesEditWin : Window
    {
        public FinishedProductsSalesEditWin(object id)
        {
            InitializeComponent();

            DataContext = new FinishedProductsSalesEditViewModel(id.ToString(), new HttpApiClient(), new DialogService());
        }
    }
}
