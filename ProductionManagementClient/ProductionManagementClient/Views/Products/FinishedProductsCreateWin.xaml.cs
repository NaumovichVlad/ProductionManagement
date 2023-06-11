using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Products;
using System.Windows;

namespace ProductionManagementClient.Views.Products
{
    /// <summary>
    /// Логика взаимодействия для FinishedProductsCreateWin.xaml
    /// </summary>
    public partial class FinishedProductsCreateWin : Window
    {
        public FinishedProductsCreateWin()
        {
            InitializeComponent();

            DataContext = new FinishedProductsCreateViewModel(new HttpApiClient(), new DialogService());
        }
    }
}
