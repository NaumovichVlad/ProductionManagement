using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Materials;
using System.Windows;

namespace ProductionManagementClient.Views.Materials
{
    /// <summary>
    /// Логика взаимодействия для CheckoutWin.xaml
    /// </summary>
    public partial class CheckoutWin : Window
    {
        public CheckoutWin()
        {
            InitializeComponent();

            DataContext = new CheckoutCreateViewModel(new HttpApiClient(), new DialogService());
        }
    }
}
