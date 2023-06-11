using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Products;
using System.Windows;

namespace ProductionManagementClient.Views.Products
{
    /// <summary>
    /// Логика взаимодействия для FinishedProductsEditWin.xaml
    /// </summary>
    public partial class FinishedProductsEditWin : Window
    {
        public FinishedProductsEditWin(object id)
        {
            InitializeComponent();

            DataContext = new FinishedProductsEditViewModel(id.ToString(), new HttpApiClient(), new DialogService());
        }
    }
}
