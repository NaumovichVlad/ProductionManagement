using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Materials;
using System.Windows;

namespace ProductionManagementClient.Views.Materials
{
    /// <summary>
    /// Логика взаимодействия для MaterialsForProductsEditWin.xaml
    /// </summary>
    public partial class MaterialsForProductsEditWin : Window
    {
        public MaterialsForProductsEditWin(object id)
        {
            InitializeComponent();

            DataContext = new MaterialsForProductsEditViewModel(id.ToString(), new HttpApiClient(), new DialogService());
        }
    }
}
