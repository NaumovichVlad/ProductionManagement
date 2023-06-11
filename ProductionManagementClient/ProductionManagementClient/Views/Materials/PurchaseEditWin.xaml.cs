using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Materials;
using System.Windows;

namespace ProductionManagementClient.Views.Materials
{
    /// <summary>
    /// Логика взаимодействия для MaterialOrderEditWin.xaml
    /// </summary>
    public partial class PurchaseEditWin : Window
    {
        public PurchaseEditWin(object id)
        {
            InitializeComponent();

            DataContext = new PurchaseEditViewModel(id.ToString(), new HttpApiClient(), new DialogService());
        }
    }
}
