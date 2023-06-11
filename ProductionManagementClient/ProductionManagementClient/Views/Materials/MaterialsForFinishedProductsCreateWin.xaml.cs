using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Materials;
using System.Windows;

namespace ProductionManagementClient.Views.Materials
{
    /// <summary>
    /// Логика взаимодействия для MaterialsForFinishedProductsCreateWin.xaml
    /// </summary>
    public partial class MaterialsForFinishedProductsCreateWin : Window
    {
        public MaterialsForFinishedProductsCreateWin()
        {
            InitializeComponent();

            DataContext = new MaterialsForFinishedProductsCreateViewModel(new HttpApiClient(), new DialogService());
        }
    }
}
