using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Products;
using System.Windows;

namespace ProductionManagementClient.Views.Products
{
    /// <summary>
    /// Логика взаимодействия для ProductsEditWin.xaml
    /// </summary>
    public partial class ProductsEditWin : Window
    {
        public ProductsEditWin(object id)
        {
            InitializeComponent();

            DataContext = new ProductsEditViewModel(id.ToString(), new HttpApiClient(), new DialogService());
        }
    }
}
