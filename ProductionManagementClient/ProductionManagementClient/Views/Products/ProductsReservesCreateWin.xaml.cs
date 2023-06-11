using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Products;
using System.Windows;

namespace ProductionManagementClient.Views.Products
{
    /// <summary>
    /// Логика взаимодействия для ProductsReservesCreateWin.xaml
    /// </summary>
    public partial class ProductsReservesCreateWin : Window
    {
        public ProductsReservesCreateWin()
        {
            InitializeComponent();

            DataContext = new ProductsReservesCreateViewModel(new HttpApiClient(), new DialogService());
        }
    }
}
