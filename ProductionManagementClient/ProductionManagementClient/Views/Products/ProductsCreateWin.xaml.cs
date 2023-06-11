using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Products;
using System.Windows;

namespace ProductionManagementClient.Views.Products
{
    /// <summary>
    /// Логика взаимодействия для ProductsCreateWin.xaml
    /// </summary>
    public partial class ProductsCreateWin : Window
    {
        public ProductsCreateWin()
        {
            InitializeComponent();

            DataContext = new ProductsCreateViewModel(new HttpApiClient(), new DialogService());
        }
    }
}
