using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Products;
using System.Windows;

namespace ProductionManagementClient.Views.Products
{
    /// <summary>
    /// Логика взаимодействия для ManufacturingNewProductWIn.xaml
    /// </summary>
    public partial class ManufacturingNewProductWIn : Window
    {
        public ManufacturingNewProductWIn()
        {
            InitializeComponent();

            DataContext = new ManufacturingNewProductViewModel(new HttpApiClient(), new DialogService());
        }
    }
}
