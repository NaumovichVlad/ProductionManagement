using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Materials;
using System.Windows;

namespace ProductionManagementClient.Views.Materials
{
    /// <summary>
    /// Логика взаимодействия для MaterialOrderCreateWin.xaml
    /// </summary>
    public partial class PurchaseCreateWin : Window
    {
        public PurchaseCreateWin()
        {
            InitializeComponent();

            DataContext = new PurchaseCreateViewModel(new HttpApiClient(), new DialogService());
        }
    }
}
