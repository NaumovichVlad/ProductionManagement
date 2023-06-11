using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Materials;
using System.Windows;

namespace ProductionManagementClient.Views.Materials
{
    /// <summary>
    /// Логика взаимодействия для MaterialPurchaseEditWin.xaml
    /// </summary>
    public partial class MaterialPurchaseEditWin : Window
    {
        public MaterialPurchaseEditWin(object id)
        {
            InitializeComponent();

            DataContext = new MaterialPurchaseEditViewModel(id.ToString(), new HttpApiClient(), new DialogService());
        }
    }
}
