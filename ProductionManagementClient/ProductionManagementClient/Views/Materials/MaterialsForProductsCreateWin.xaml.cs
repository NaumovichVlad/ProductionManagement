using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Materials;
using System.Windows;

namespace ProductionManagementClient.Views.Materials
{
    /// <summary>
    /// Логика взаимодействия для MaterialsForProductsCreateWin.xaml
    /// </summary>
    public partial class MaterialsForProductsCreateWin : Window
    {
        public MaterialsForProductsCreateWin()
        {
            InitializeComponent();

            DataContext = new MaterialsForProductsCreateViewModel(new HttpApiClient(), new DialogService());
        }
    }
}
