using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Products;
using System.Windows;

namespace ProductionManagementClient.Views.Products
{
    /// <summary>
    /// Логика взаимодействия для FinishedProductsSalesCreateWin.xaml
    /// </summary>
    public partial class FinishedProductsSalesCreateWin : Window
    {
        public FinishedProductsSalesCreateWin()
        {
            InitializeComponent();

            DataContext = new FinishedProductsSalesCreateViewModel(new HttpApiClient(), new DialogService());
        }
    }
}
