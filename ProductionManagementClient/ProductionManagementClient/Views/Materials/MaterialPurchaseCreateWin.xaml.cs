using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Materials;
using System.Windows;

namespace ProductionManagementClient.Views.Materials
{
    /// <summary>
    /// Логика взаимодействия для MaterialPurchaseCreateWin.xaml
    /// </summary>
    public partial class MaterialPurchaseCreateWin : Window
    {
        public MaterialPurchaseCreateWin()
        {
            InitializeComponent();

            DataContext = new MaterialPurchaseCreateViewModel(new HttpApiClient(), new DialogService());
        }
    }
}
