using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Products;
using System.Windows;

namespace ProductionManagementClient.Views.Products
{
    /// <summary>
    /// Логика взаимодействия для SalesRegistrationCreateWin.xaml
    /// </summary>
    public partial class SalesRegistrationCreateWin : Window
    {
        public SalesRegistrationCreateWin()
        {
            InitializeComponent();

            DataContext = new SalesRegistrationCreateViewModel(new HttpApiClient(), new DialogService());
        }
    }
}
