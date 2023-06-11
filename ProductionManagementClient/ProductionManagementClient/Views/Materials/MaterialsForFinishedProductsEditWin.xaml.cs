using ProductionManagementClient.Connection;
using ProductionManagementClient.Services;
using ProductionManagementClient.ViewModels.Materials;
using System.Windows;

namespace ProductionManagementClient.Views.Materials
{
    /// <summary>
    /// Логика взаимодействия для MaterialsForFinishedProductsEditWin.xaml
    /// </summary>
    public partial class MaterialsForFinishedProductsEditWin : Window
    {
        public MaterialsForFinishedProductsEditWin(object id)
        {
            InitializeComponent();

            DataContext = new MaterialsForFinishedProductsEditViewModel(id.ToString(), new HttpApiClient(), new DialogService());
        }
    }
}
