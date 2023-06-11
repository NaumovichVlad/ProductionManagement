using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Products;
using System.Windows;

namespace ProductionManagementClient.Views.Products
{
    /// <summary>
    /// Логика взаимодействия для SalesEditWIn.xaml
    /// </summary>
    public partial class SalesEditWIn : Window
    {
        public SalesEditWIn(object id)
        {
            InitializeComponent();

            DataContext = new SalesEditViewModel(id.ToString(), new HttpApiClient(), new DialogService());
        }
    }
}
