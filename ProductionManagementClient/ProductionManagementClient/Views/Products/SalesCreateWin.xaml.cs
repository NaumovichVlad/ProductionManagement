using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Products;
using System.Windows;

namespace ProductionManagementClient.Views.Products
{
    /// <summary>
    /// Логика взаимодействия для SalesCreateWin.xaml
    /// </summary>
    public partial class SalesCreateWin : Window
    {
        public SalesCreateWin()
        {
            InitializeComponent();

            DataContext = new SalesCreateViewModel(new HttpApiClient(), new DialogService());
        }
    }
}
